using Usda.PlantBreeding.Core.Interfaces;
using Usda.PlantBreeding.Core.Models;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Core.Translations;
using Usda.PlantBreeding.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using BSGUtils;
using System.Web.Mvc;
using AutoMapper;

namespace Usda.PlantBreeding.Core.Infrastructure
{
    public class AccessionUOW : IAccessionUOW
    {
        private IPlantBreedingRepo u_repo;
        public AccessionUOW() : this(new PlantBreedingRepo()) { }

        public AccessionUOW(IPlantBreedingRepo repo)
        {
            u_repo = repo;
        }

        public AccessionViewModel GetAccessionViewModel(int id)
        {
            Genotype genotype = u_repo.GetGenotype(id);
            if (genotype == null) { return null; }

            IEnumerable<MapComponent> mapComponents = genotype.MapComponents.OrderBy(t => t.Map.EvaluationYear);


            var accession = genotype.ToAccessionViewModel();
            if (accession.IsBase.Equals(true))
            {
                accession.Year = null;
                accession.Note = null;
            }
            accession.MapComponents = mapComponents.Where(t => t.Map.Retired == false).OrderByDescending(t => t.Map.PlantingYear).ToList();
            accession.MapComponentsRetired = mapComponents.Where(t => t.Map.Retired == true).OrderByDescending(t => t.Map.PlantingYear).ToList();

            return accession;
        }

        public Genotype CreateGenotypeSelection(int genotypeId)
        {
            Genotype genotype = u_repo.GetGenotype(genotypeId);
            if (genotype == null || genotype.FamilyId <= 0)
            {
                throw new AccessionException("Unable to create selection");
            }

            int SelectionNum = genotype.Family.Genotypes.Max(x => x.SelectionNum).GetValueOrDefault() + 1;
            Genotype newSelection = new Genotype()
            {
                Family = genotype.Family,
                FamilyId = genotype.FamilyId,
                Year = DateTime.Now.Year.ToString(),
                SelectionNum = SelectionNum,
                IsRoot = false
            };
            u_repo.SaveGenotype(newSelection);

            return newSelection;
        }

        public void SaveAccessionViewModel(AccessionViewModel accession)
        {
            if (accession.Id <= 0 && accession.FamilyId <= 0)
            {
                createAccessionPopulation(accession);
            }
            else
            {
                updateAccession(accession);
            }
        }

        public void SaveGenotype(Genotype genotype)
        {
            u_repo.SaveGenotype(genotype);
        }
        /// <summary>
        /// Updates a genotype from a crossplan
        /// </summary>
        /// <param name="cp"></param>
        public void UpdateAccessionFromCrossPlan(CrossPlan cp)
        {
            if (!cp.GenotypeId.HasValue)
            {
                //TODO: Throw error
                return;
            }

            Genotype genotype = u_repo.GetGenotype(cp.GenotypeId.Value);
            if (genotype != null)
            {
                genotype.UpdateFromCrossPlan(cp);
                u_repo.SaveGenotype(genotype);
            }

        }

        /// <summary>
        /// Get the next available accession
        /// </summary>
        /// <param name="id">Genus Id of next accession</param>
        /// <returns></returns>
        public AccessionViewModel GetNextAccession(int id)
        {
            AccessionViewModel accession = new AccessionViewModel
            {
                FamilyGenusId = id,
                FamilyBaseGenotypeYear = DateTime.Now.Year.ToString(),
                IsBase = true,
                MapComponents = new List<MapComponent>()
            };
            return accession;
        }

        /// <summary>
        /// Gets the next available accession with origin and next available cross number
        /// </summary>
        /// <param name="id">genus id of the accession</param>
        /// <param name="originId">origin id to get the next available number</param>
        /// <returns></returns>
        public AccessionViewModel GetNextAccession(int id, int originId)
        {
            Origin origin = u_repo.GetOrigin(originId);
            AccessionViewModel accession = GetNextAccession(id);
            accession.FamilyCrossNum = GetNextCross(origin.Id, id);
            accession.FamilyOriginId = origin.Id;
            accession.FamilyOriginName = origin.Name;
            return accession;
        }

        public string GetNextCross(int originId, int genusId)
        {
            string result = "";
            int crossVal = 0;
            string crossnum = u_repo.GetQueryableFamilies(t => t.OriginId == originId && t.GenusId == genusId)
                .OrderByDescending(f => f.CrossNum.Length)
                .ThenByDescending(f => f.CrossNum)
                .FirstOrDefault()
                .CrossNum;
            int.TryParse(crossnum, out crossVal);
            if (crossVal != 0)
            {
                crossVal++;
                result = crossVal.ToString();
            }
            return result;
        }

        public int GetNextSelection(int familyId)
        {
            int result = u_repo.GetGenotypes(g => g.FamilyId == familyId && g.SelectionNum.HasValue)
                .OrderByDescending(g => g.SelectionNum)
                .FirstOrDefault()
                .SelectionNum
                .Value;
            return ++result;
        }

        public bool IsDuplicateSelection(int genotypeId, int selection, int familyId)
        {
            bool val = false;
            if (familyId > 0)
            {
                int count = u_repo.GetGenotypes(g => g.FamilyId == familyId && g.Id != genotypeId
                                    && g.SelectionNum.HasValue && g.SelectionNum == selection).Count();
                val = count > 0;
            }

            return val;
        }

        public bool IsDuplicatePopulation(int familyId, int originId, string cross, int genusId)
        {
            int count = u_repo.GetFamilies(f => f.GenusId == genusId && f.Id != familyId && f.OriginId == originId && f.CrossNum == cross)
                .Count();
            return count > 0;
        }

        private void createAccessionPopulation(AccessionViewModel accession)
        {
            Family family = accession.ToFamily();

            try
            {
                u_repo.SaveFamily(family);
            }
            catch (Exception e)
            {
                //TODO:write to logging
                throw new AccessionException("Unable to create population");
            }


            Genotype genotype = accession.ToGenotype();
            genotype.Family = family;
            genotype.FamilyId = family.Id;

            if (accession.IsBase)
            {
                genotype.Year = accession.FamilyBaseGenotypeYear;
                genotype.Note = accession.FamilyBaseGenotypeNote;
                genotype.IsRoot = true;
            }

            try
            {
                u_repo.SaveGenotype(genotype);
            }
            catch (Exception e)
            {
                u_repo.DeleteFamily(family);
                //TODO:write to logging
                throw new AccessionException("Unable to create selection");
            }

            family.BaseGenotype = genotype;
            family.BaseGenotypeId = genotype.Id;

            u_repo.SaveFamily(family);
            accession.Id = genotype.Id;
        }

        private void updateAccession(AccessionViewModel accession)
        {
            // update genotype information
            Genotype oldGenotype = u_repo.GetGenotype(accession.Id);
            var oldGivenName = oldGenotype.GivenName;
            var oldOriginalName = oldGenotype.OriginalName;

            Genotype genotype = accession.ToGenotype();
            genotype.Family = oldGenotype.Family;
            genotype.FamilyId = oldGenotype.FamilyId;
            genotype.CrossPlanId = oldGenotype.CrossPlanId;
            genotype.IsPopulation = accession.IsPopulation;


            if (accession.Id <= 0)
            {
                genotype.Family = u_repo.GetFamily(genotype.FamilyId);
            }

            if (accession.IsBase)
            {
                genotype.Year = accession.FamilyBaseGenotypeYear;
                genotype.Note = accession.FamilyBaseGenotypeNote;
                genotype.IsRoot = true;
            }


            if (genotype.GivenName != oldGivenName && !oldGivenName.IsNullOrWhiteSpace())
            {
                genotype.Alias2 = genotype.Alias1;
                genotype.Alias1 = oldGivenName;
            }

            // update family information
            if (accession.IsBase)
            {
                Family oldFamily = u_repo.GetFamily(accession.FamilyId);
                // check origin name and cross number

                Family family = accession.ToFamily();
                if (family.CrossTypeId.HasValue)
                {
                    family.CrossType = u_repo.GetCrossType(accession.FamilyCrossTypeId.Value);

                }

                if (family.OriginId.HasValue)
                {
                    family.Origin = u_repo.GetOrigin(accession.FamilyOriginId.Value);

                }

                if (oldFamily.OriginalName != family.OriginalName && !oldFamily.OriginalName.IsNullOrWhiteSpace())
                {
                    genotype.Alias2 = genotype.Alias1;
                    genotype.Alias1 = oldOriginalName;
                    u_repo.UpdateGeotypesAlias(family.Id, oldOriginalName);
                }

                family.BaseGenotype = oldFamily.BaseGenotype;
                family.BaseGenotypeId = oldFamily.BaseGenotypeId;
                family.Genus = oldFamily.Genus;
                family.Genotypes = oldFamily.Genotypes;
                if (family.FemaleParent.HasValue)
                {
                    family.FemaleGenotype = u_repo.GetGenotype(family.FemaleParent.Value);
                }
                if (family.MaleParent.HasValue)
                {
                    family.MaleGenotype = u_repo.GetGenotype(family.MaleParent.Value);
                }

                if (family.OriginId.HasValue)
                {
                    family.Origin = u_repo.GetOrigin(family.OriginId.Value);

                }
                u_repo.SaveFamily(family);
            }
            u_repo.SaveGenotype(genotype);

            if (genotype.CrossPlanId.HasValue)
            {
                UpdateCrossPlanFromAccession(genotype);
            }

        }


        public void UpdateCrossPlanFromAccession(Genotype genotype)
        {
            if (!genotype.CrossPlanId.HasValue)
            {
                throw new CrossPlanException(Properties.Settings.Default.ExceptionCrossPlanAccessionNoId);
            }

            CrossPlan crossplan = u_repo.GetCrossPlan(genotype.CrossPlanId.Value);
            if (crossplan != null)
            {
                crossplan.UpdateFromAccession(genotype);
                u_repo.SaveCrossPlan(crossplan);
            }
        }



        private string getOldName(Genotype g)
        {
            Family f = g.Family;
            string s = f.Origin.Name + " " + f.CrossNum;
            if (f.ReciprocalString != "" && !f.ReciprocalString.ToLower().Equals("n"))
            {
                s += f.ReciprocalString;
            }
            s += "-" + g.SelectionNum.ToString();
            return s;
        }

        public Genotype GetGenotype(int id)
        {
            return u_repo.GetGenotype(id);
        }


        public void RestoreProperties(AccessionViewModel accession)
        {
            AccessionViewModel oldAccession = GetAccessionViewModel(accession.Id);
            // restore map components
            accession.MapComponents = oldAccession.MapComponents;
            // restore family properties if not base
            if (!accession.IsBase)
            {
                // Genotype genotype = u_repo.GetGenotype(accession.Id);
                // Family family = genotype.Family;
                Family family = u_repo.GetFamily(accession.FamilyId);
                Mapper.Map(family, accession);
            }
        }

        #region Search
        public bool isReasonableRequest(int page, int total, int pageSize)
        {
            // total records is divisable by page size
            bool p = total % pageSize == 0 && total / pageSize - page >= 0;
            // total records is not divisable by page records
            bool q = total % pageSize != 0 && total / pageSize + 1 - page >= 0;
            // is request is reasonable?
            return pageSize > 0 && page > 0 && (p || q);
        }

        /// <summary>
        /// Finds and returns the page number for the given genotype.
        /// </summary>
        /// <param name="genotypes">
        /// Collection of genotypes to fill pages.
        /// </param>
        /// <param name="id">
        /// ID of genotype to search for.
        /// </param>
        /// <param name="pageSize">
        /// Number of genotypes per page.
        /// </param>
        /// <param name="genusId">
        /// Genus ID.
        /// </param>
        /// <param name="searchterm">
        /// Name of genotype to search for.
        /// </param>
        /// <returns></returns>
        private int? FindGenotypeInPage(IQueryable<Genotype> genotypes, int? id, int pageSize, int genusId, string searchterm = null)
        {
            int? result = null;
            Genotype genotype;
            if (id.HasValue)
            {
                genotype = u_repo.GetGenotype(id.Value);
            }
            else
            {
                genotype = u_repo.GetGenotypes(searchterm, genusId, 1).FirstOrDefault();
            }
            if (genotype != null)
            {
                int genotypeIndex = genotypes.ToList().FindIndex(t => t.Id == genotype.Id) + 1;
                if (pageSize > 0)
                {
                    result = (int)Math.Ceiling((decimal)genotypeIndex / pageSize);
                }
            }
            return result;
        }

        /// <summary>
        /// Returns page of accessions.
        /// </summary>
        /// <param name="pageSize">
        /// Number of genotypes per page.
        /// </param>
        /// <param name="genotypeId">
        /// ID of genotype to search for.
        /// </param>
        /// <param name="filter"></param>
        /// <param name="page">
        /// Page number.
        /// </param>
        /// <param name="genusId">
        /// Genus ID.
        /// </param>
        /// <returns></returns>
        public IPagedList<AccessionIndexViewModel> GetPageInIndex(int pageSize, int? genotypeId, string filter, int page, int genusId)
        {
            IQueryable<Genotype> genotypes = u_repo.GetQueryableGenotypes(g => g.Family.GenusId == genusId);
            filter = filter.TrimAndRemoveDoubleSpaces();
            if (!filter.IsNullOrWhiteSpace() || genotypeId.HasValue)
            {
                int? temp = FindGenotypeInPage(genotypes, genotypeId, pageSize, genusId, filter);
                if (temp.HasValue)
                {
                    page = temp.Value;
                }
                else
                {
                    throw new SearchException(Properties.Settings.Default.ExceptionAccessionsInvalidSearch);
                }
            }

            int total = genotypes.Count();
            if (pageSize == 0)
            {
                pageSize = total;
            }

            if (!isReasonableRequest(page, total, pageSize))
            {
                throw new SearchException(Properties.Settings.Default.ExceptionInvalidPage);
            }

            return GetAccessionIndexViewModel(genotypes, page, pageSize);
        }
        public IPagedList<AccessionIndexViewModel> GetPageInIndex(int pageSize, int page, int genusId)
        {
            IQueryable<Genotype> genotypes = u_repo.GetQueryableGenotypes(g => g.Family.GenusId == genusId);
            int total = genotypes.Count();

            if (!isReasonableRequest(page, total, pageSize))
            {
                throw new SearchException(Properties.Settings.Default.ExceptionInvalidPage);
            }

            return GetAccessionIndexViewModel(genotypes, page, pageSize);

        }



        public IPagedList<AccessionIndexViewModel> GetAccessionIndexViewModel(IQueryable<Genotype> genotypes, int page, int pageSize)
        {
            var accessions = genotypes.Select(t => new AccessionIndexViewModel()
            {
                Id = t.Id,
                FamilyId = t.FamilyId,
                GivenName = t.GivenName,
                OriginalName = t.OriginalName,
                SelectionNum = t.SelectionNum,
                FamilyOriginName = t.Family.Origin.Name,
                FamilyCrossNum = t.Family.CrossNum,
                Year = t.Year,
                IsPopulation = t.IsPopulation,
                FamilyCrossTypeName = t.Family.CrossType.Name,
                PloidyName = t.PloidyName,
                Note = t.Note,
                FamilyPurpose = t.Family.Purpose,
                FamilyReciprocalString = t.Family.ReciprocalString,

                FamilyFemaleParent = t.Family.FemaleParent,
                FamilyMaleParent = t.Family.MaleParent,
                FamilyFemaleGenotypeGivenName = t.Family.FemaleGenotype.GivenName,
                FamilyFemaleParentOriginalName = t.Family.FemaleParentOriginalName,
                FamilyMaleParentOriginalName = t.Family.MaleParentOriginalName,
                FamilyMaleGenotypeGivenName = t.Family.MaleGenotype.GivenName,
            }).ToList();


            return accessions.ToPagedList(page, pageSize);
        }

        public IEnumerable<DataListViewModel> Search(string term, int genusId, int? mapId, bool mapOnly, int recordsToReturn)
        {
            term = term.TrimAndRemoveDoubleSpaces();

            IEnumerable<Genotype> genotypes;
            if (mapId.HasValue && mapId.Value > 0)
            {
                if (mapOnly)
                {
                    genotypes = u_repo.GetGenotypes(term, genusId, recordsToReturn, mapId.Value, mapOnly);
                }
                else
                {
                    genotypes = u_repo.GetGenotypes(term, genusId, recordsToReturn, mapId.Value);
                }
            }
            else
            {
                genotypes = u_repo.GetGenotypes(term, genusId, recordsToReturn);
            }

            return genotypes.ToDataListViewModel();
        }

        public IEnumerable<DataListViewModel> Search(string term, int genusId, int recordsToReturn)
        {
            term = term.TrimAndRemoveDoubleSpaces();

            List<AccessionSearch> accessions = u_repo.GetQueryableGenotypes(term, genusId)
                                                            .Select(t => new AccessionSearch()
                                                            {
                                                                GenotypeId = t.Id,
                                                                OriginalName = t.OriginalName,
                                                                GivenName = t.GivenName,
                                                                Alias1 = t.Alias1,
                                                                Alias2 = t.Alias2
                                                            })
                                                            .Take(recordsToReturn).ToList();
            return accessions.ToDataListViewModel();
        }

        public void UpdateGenotypeNote(int genotypeId, string note)
        {
            var genotype = u_repo.GetGenotype(genotypeId);

            if (genotype == null)
            {
                throw new AccessionException("Invalid Genotype when saving");
            }

            genotype.Note = note;
            u_repo.DoSaveGenotype(genotype);
        }

        public void UpdateGenotypePloidy(int genotypeId, string ploidy)
        {
            var genotype = u_repo.GetGenotype(genotypeId);

            if (genotype == null)
            {
                throw new AccessionException("Invalid Genotype when saving");
            }

            genotype.PloidyName = ploidy;
            u_repo.DoSaveGenotype(genotype);

        }
        public void UpdateGenotypeIsPopulation(int genotypeId, bool isPopulation)
        {
            var genotype = u_repo.GetGenotype(genotypeId);

            if(genotype == null)
            {
                throw new AccessionException("Invalid Genotype when saving");
            }

            genotype.IsPopulation = isPopulation;
            u_repo.DoSaveGenotype(genotype);
        }

        private Family CopyFamily(Family f)
        {
            Family family = new Family()
            {
                CrossNum = f.CrossNum,
                CrossType = f.CrossType,
                CrossTypeId = f.CrossTypeId,
                FemaleGenotype = f.FemaleGenotype,
                FemaleParent = f.FemaleParent,
                Genus = f.Genus,
                GenusId = f.GenusId,
                MaleGenotype = f.MaleGenotype,
                MaleParent = f.MaleParent,
                FieldPlantedNum = f.FieldPlantedNum,
                Origin = f.Origin,
                OriginId = f.OriginId,
                Purpose = f.Purpose,
                IsReciprocal = f.IsReciprocal,
                ReciprocalString = f.ReciprocalString,
                SeedNum = f.SeedNum,
                TransplantedNum = f.TransplantedNum,
                Unsuccessful = f.Unsuccessful
            };

            return family;
        }


        public void CreatePopulationFromGenotype(int genotypeId)
        {
            var genotype = u_repo.GetGenotype(genotypeId);
            if (genotype == null) { throw new ArgumentNullException(); }

            var oldFamily = u_repo.GetFamily(genotype.FamilyId);
            genotype.Family = null;
            genotype.FamilyId = -1;

            var newFamily = CopyFamily(oldFamily);

            newFamily.BaseGenotype = genotype;
            newFamily.BaseGenotypeId = genotype.Id;

            genotype.Family = newFamily;
            genotype.FamilyId = newFamily.Id;

            u_repo.DoSaveFamily(newFamily);
            u_repo.SaveGenotype(genotype);
        }

        public void DeleteSelection(int genotypeId)
        {
            var genotype = u_repo.GetGenotype(genotypeId);
            if (genotype == null) { throw new ArgumentNullException(); }

            //Check if the genotype belongs to anything

            if (genotype.IsBase ||
                genotype.FemaleofFamilies.Any() ||
                 genotype.MaleofFamilies.Any() ||
                 genotype.MapComponents.HasAny(m => m.isSeedling == true) ||
                 u_repo.GetCrossPlans(
                     c => c.MaleParentId == genotype.Id ||
                     c.FemaleParentId == genotype.Id).Any())
            {
                throw new AccessionException("Selection is referenced in the DB");
            }
            else
            {
                u_repo.DeleteGenotypeWithRelated(genotype);
            }

        }

        #endregion
    }
}
