using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usda.PlantBreeding.Core.Interfaces;
using Usda.PlantBreeding.Core.Models;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Core.Translations;
using System.Web.Mvc;
using Usda.PlantBreeding.Core.Extensions;
using BSGUtils;
namespace Usda.PlantBreeding.Core.Infrastructure
{
    public class CrossPlanUOW : ICrossPlanUOW
    {
        private IPlantBreedingRepo u_repo;
        private IAccessionUOW a_repo;

        public CrossPlanUOW() : this(new PlantBreedingRepo()) { }


        public CrossPlanUOW(IPlantBreedingRepo repo)
        {
            u_repo = repo;
            a_repo = new AccessionUOW(repo);
        }


        public IEnumerable<DataListViewModel> SearchAccessions(string term, int genusId, int recordsToReturn)
        {
            return a_repo.Search(term, genusId, recordsToReturn);
        }
        public AccessionViewModel GetNextAccession(int crossId)
        {
            CrossPlan crossPlan = u_repo.GetCrossPlan(crossId);
            int genusId = crossPlan.GenusId;
            Origin origin = u_repo.GetDefaultOrigin();
            AccessionViewModel accession = a_repo.GetNextAccession(genusId, origin.Id);
            accession.CopyCrossToAccession(crossPlan);
            accession.IsBase = true;
            accession.IsRoot = true;
            accession.Id = 0;
            return accession;
        }

        public void Save(CrossPlanViewModel crossPlan)
        {
            if (crossPlan.Id > 0)
            {
                var oldplan = u_repo.GetCrossPlan(crossPlan.Id);
                if (oldplan == null)
                {
                    throw new CrossPlanException("Invalid Plan");
                }
                oldplan.CopyCrossPlanViewModel(crossPlan);
                Save(oldplan, false);
            }
            else
            {
                CrossPlan cross = crossPlan.ToCrossPlan();
                Save(cross);
            }

        }
        public void Save(CrossPlan crossPlan)
        {
            Save(crossPlan, true);
        }

        public void Save(CrossPlan crossPlan, bool restoreDefaults)
        {
            if (restoreDefaults)
            {
                GetDefaultValues(crossPlan);

            }
            u_repo.SaveCrossPlan(crossPlan);
            if (crossPlan.GenotypeId.HasValue)
            {
                UpdateAccessionFromCrossPlan(crossPlan);
            }
        }

        /// <summary>
        /// Refreshes properties before saving to the repo layer. This method is needed to restore references
        /// </summary>
        /// <param name="crossPlan"></param>
        private void GetDefaultValues(CrossPlan crossPlan)
        {
            GetDefaultValues(crossPlan, u_repo);

        }

        private void GetDefaultValues(CrossPlan crossPlan, IPlantBreedingRepo repo)
        {
            if (crossPlan.Id > 0 && !crossPlan.GenotypeId.HasValue)
            {
                CrossPlan old = repo.GetCrossPlan(crossPlan.Id);
                if (!crossPlan.GenotypeId.HasValue)
                {
                    crossPlan.GenotypeId = old.GenotypeId;

                }
            }


            if (crossPlan.FemaleParentId.HasValue && crossPlan.FemaleParent == null)
            {
                crossPlan.FemaleParent = repo.GetGenotype(crossPlan.FemaleParentId.Value);
            }

            if (crossPlan.MaleParentId.HasValue && crossPlan.MaleParent == null)
            {
                crossPlan.MaleParent = repo.GetGenotype(crossPlan.MaleParentId.Value);
            }

            if (crossPlan.OriginId.HasValue && crossPlan.OriginId == null)
            {
                crossPlan.Origin = repo.GetOrigin(crossPlan.OriginId.Value);
            }

            if (crossPlan.CrossTypeId.HasValue && crossPlan.CrossType == null)
            {
                crossPlan.CrossType = repo.GetCrossType(crossPlan.CrossTypeId.Value);
            }

            crossPlan.Genus = repo.GetGenus(crossPlan.GenusId);

        }


        /// <summary>
        /// Updates a genotype from a crossplan
        /// </summary>
        /// <param name="cp"></param>
        public Genotype GenotypeFromCrossPlan(CrossPlan cp)
        {
            if (!cp.GenotypeId.HasValue)
            {
                //TODO: Throw error

            }

            Genotype genotype = u_repo.GetGenotype(cp.GenotypeId.Value);
            if (genotype != null)
            {
                genotype.UpdateFromCrossPlan(cp);
            }
            return genotype;

        }

        public void GenotypeFromCrossPlan(CrossPlan cp, Genotype gen)
        {


            if (gen != null)
            {
                gen.UpdateFromCrossPlan(cp);
            }


        }

        public void UpdateAccessionFromCrossPlan(CrossPlan cp)
        {
            Genotype genotype = GenotypeFromCrossPlan(cp);
            u_repo.SaveGenotype(genotype);
        }

        public void CopyToNewYear(CrossPlanViewModel cpvm)
        {
            var crossplan = cpvm.ToCrossPlan();
            CopyToNextYear(crossplan);
        }

        public void CopyToNewYear(CrossPlan old, string year)
        {
            if (year.IsNullOrWhiteSpace())
            {
                throw new CrossPlanException(Properties.Settings.Default.ExceptionCrossPlanInvalidYear);
            }

            CrossPlan copyPlan = GetNext(year, old.GenusId);
            copyPlan.CopyCrossPlan(old);
            u_repo.SaveCrossPlan(copyPlan);
        }

        public void CopyToNextYear(CrossPlan old)
        {
            if (old.Year.IsNullOrWhiteSpace())
            {
                throw new CrossPlanException(Properties.Settings.Default.ExceptionCrossPlanInvalidYear);
            }

            int yearVal = 0;
            int.TryParse(old.Year, out yearVal);
            if (yearVal == 0)
            {
                throw new CrossPlanException(Properties.Settings.Default.ExceptionCrossPlanInvalidYear);
            }

            yearVal++;
            CopyToNewYear(old, yearVal.ToString());



        }

        public void SaveIndex(CrossPlanIndexViewModel crossIndex)
        {
            List<CrossPlan> allPlans = crossIndex.CrossPlans.ToCrossPlan().ToList();
            List<CrossPlan> crossPlans = new List<CrossPlan>();
            List<Task> saveCrossPlansTasks = new List<Task>();
            int size = 12;
            foreach (CrossPlan cp in allPlans)
            {
                if (!IsEmptyCrossPlan(cp))
                {
                    crossPlans.Add(cp);
                }
            }

            List<List<CrossPlan>> planLists = crossPlans.Partition(size).ToList();
            foreach (List<CrossPlan> cplan in planLists)
            {
                Task task = new Task(() => SaveCrossPlanAsync(cplan));
                task.Start();
                saveCrossPlansTasks.Add(task);
            }
            Task updateAccessionTask = new Task(() => UpdateAccessionFromCrossPlan(crossPlans));
            updateAccessionTask.Start();

            Task.WaitAll(saveCrossPlansTasks.ToArray());

        }
        


        public void UpdateAccessionFromCrossPlan(List<CrossPlan> crossPlans)
        {
            using (IPlantBreedingRepo repo = new PlantBreedingRepo())
            {
                foreach (CrossPlan cp in crossPlans)
                {
                    if (cp.GenotypeId.HasValue)
                    {
                        Genotype gen = repo.GetGenotype(cp.GenotypeId.Value);
                        gen.UpdateFromCrossPlan(cp);

                        if (cp.CrossTypeId.HasValue)
                        {
                            gen.Family.CrossType = repo.GetCrossType(cp.CrossTypeId.Value);
                        }

                        repo.SaveGenotype(gen);
                    }

                }
            }


        }


        public void SaveCrossPlanAsync(List<CrossPlan> crossplans)
        {
            using (IPlantBreedingRepo repo = new PlantBreedingRepo())
            {
                foreach (CrossPlan cp in crossplans)
                {
                    GetDefaultValues(cp, repo);
                }
                repo.SaveCrossPlan(crossplans);
            }
        }

        private bool IsEmptyCrossPlan(CrossPlan cp)
        {
            bool val = true;
            if (cp.Id > 0 || cp.CrossTypeId.HasValue || cp.FemaleParentId.HasValue || cp.MaleParentId.HasValue || !string.IsNullOrWhiteSpace(cp.Purpose))
            {
                val = false;
            }
            return val;
        }


        private bool IsEmptyCrossPlan(CrossPlanViewModel cp)
        {
            bool val = true;
            if (cp.CrossTypeId.HasValue || cp.FemaleParentId.HasValue || cp.MaleParentId.HasValue || !string.IsNullOrWhiteSpace(cp.Purpose))
            {
                val = false;
            }
            return val;
        }


        public ParentViewModel GetParents(int genotypeId)
        {
            var genotype = u_repo.GetGenotype(genotypeId);
            ParentViewModel pvm = new ParentViewModel();
            if (genotype != null)
            {
                if (genotype.Family.MaleParent.HasValue)
                {
                    pvm.MaleParentName = genotype.Family.MaleGenotype.Name;
                }
                if (genotype.Family.FemaleParent.HasValue)
                {
                    pvm.FemaleParentName = genotype.Family.FemaleGenotype.Name;
                }
            }
            return pvm;
        }


        public void SaveFamily(int crossPlanId)
        {
            AccessionViewModel vm = GetNextAccession(crossPlanId);
            SaveAccession(vm);
        }


        public void SaveAccession(AccessionViewModel accession)
        {
            if (!accession.CrossPlanId.HasValue)
            {
                throw new CrossPlanException(Properties.Settings.Default.ExceptionCrossPlanAccessionNoId);

            }
            if (!accession.CrossPlanId.HasValue || accession.FamilyCrossNum.IsNullOrWhiteSpace())
            {
                throw new CrossPlanException("Invalid Cross Number");

            }
            if (a_repo.IsDuplicatePopulation(accession.FamilyId, accession.FamilyOriginId.Value, accession.FamilyCrossNum, accession.FamilyGenusId))
            {
                throw new CrossPlanException("Invalid Cross Number");
            }


            a_repo.SaveAccessionViewModel(accession);
            CrossPlan cp = u_repo.GetCrossPlan(accession.CrossPlanId.Value);
            cp.GenotypeId = accession.Id;
            u_repo.SaveCrossPlan(cp);

        }

        public void SaveAccession(CrossPlanViewModel cpvm)
        {
            var accession = GetNextAccession(cpvm.Id);

            if (!accession.CrossPlanId.HasValue)
            {
                throw new CrossPlanException(Properties.Settings.Default.ExceptionCrossPlanAccessionNoId);

            }

            a_repo.SaveAccessionViewModel(accession);
            CrossPlan cp = u_repo.GetCrossPlan(cpvm.Id);
            cp.GenotypeId = accession.Id;
            u_repo.SaveCrossPlan(cp);
            cpvm = cp.ToCrossPlanningViewModel();

        }

        public IEnumerable<SelectListItem> GetYears(string selectedYear, int genusId)
        {
            int yearVal = DateTime.Now.Year;
            int.TryParse(selectedYear, out yearVal);

            int currentYear = DateTime.Now.Year - 2;
            var items = Enumerable.Range(currentYear, 5).Select(t => t.ToString()).ToList();
            var pastItems = u_repo.GetCrossPlans(t => t.GenusId == genusId).Select(t => t.Year).Distinct().ToList();
            var allItems = items.Union(pastItems).Distinct();

            return allItems.Select(t => new SelectListItem
                                                {
                                                    Text = t,
                                                    Value = t,
                                                    Selected = (t == yearVal.ToString())
                                                }).OrderByDescending(t => t.Value);
        }
        public void Delete(CrossPlanViewModel crossPlan)
        {
            CrossPlan cross = u_repo.GetCrossPlan(crossPlan.Id);
            u_repo.DeleteCrossPlan(cross);
        }

        public CrossPlanViewModel GetNextCrossPlan(string year, int genusId)
        {
            CrossPlan cross = GetNext(year, genusId);
            return cross.ToCrossPlanningViewModel();
        }

        public CrossPlanViewModel CreateNextCrossPlan(string year, int genusId)
        {
            if (CanAddRow(year, genusId))
            {
                var plan = GetNext(year, genusId);
                u_repo.SaveCrossPlan(plan);
                return plan.ToCrossPlanningViewModel();
            }
            else
            {
                throw new CrossPlanException("Cannot add previous year crosses");
            }
        }

        public CrossPlan GetNext(string year, int genusId)
        {
            Genus genus = u_repo.GetGenus(genusId);
            CrossPlan cross = new CrossPlan()
            {
                Genus = genus,
                GenusId = genus.Id,
                Year = year,
                DateCreated = System.DateTime.MinValue
            };
            return cross;

        }

        public CrossPlanIndexViewModel GetIndex(string year, int genusId)
        {

            Origin defaultOrigin = u_repo.GetDefaultOrigin();
            var plans = GetCrossPlans(year, genusId).ToList();
            var nextPlan = GetNextCrossPlan(year, genusId);
        
            if ((!plans.HasAny() || !plans.HasAny(t => t.FemaleParentId.HasValue == false && t.MaleParentId.HasValue == false)) && CanAddRow(year, genusId))
            {
                var empty = CreateNextCrossPlan(year, genusId);
                plans.Add(empty);
            }

            var crossTypes = GetCrossTypesList(genusId);
            nextPlan.newIndex = plans.Count();
            return new CrossPlanIndexViewModel()
                                            {
                                                CrossPlans = plans,
                                                NewCrossPlan = nextPlan,
                                                Years = GetYears(year, genusId),
                                                CurrentYear = year,
                                                GenusId = genusId,
                                                DefaultOriginName = defaultOrigin.Name,
                                                NextCrossNumber = a_repo.GetNextCross(defaultOrigin.Id, genusId),
                                                CrossTypes = crossTypes,
                                                CrossTypesOrder = GetCrossTypeListOrder(crossTypes)
                                            };

        }
        public IEnumerable<CrossPlanViewModel> GetCrossPlans(string year, int genusId)
        {
            IQueryable<CrossPlan> plans = u_repo.GetQueryableCrossPlans(t => t.GenusId == genusId && t.Year == year);
            var viewModels = plans.ToList().Select(t => new CrossPlanViewModel()
            {
                CrossNum = t.CrossNum,
                CrossTypeId = t.CrossTypeId,
                TransplantedNum = t.TransplantedNum,
                CrossTypeName = t.CrossType?.Name,
                FemaleParentName = (t.FemaleParent?.GivenName == null || t.FemaleParent?.GivenName == string.Empty) ? t.FemaleParent?.OriginalName : t.FemaleParent.GivenName,
                MaleParentName = (t.MaleParent?.GivenName == null || t.MaleParent?.GivenName == string.Empty) ? t.MaleParent?.OriginalName : t.MaleParent?.GivenName,
                FemaleParentFemaleParent = (t.FemaleParent?.Family?.FemaleGenotype?.GivenName == null || t.FemaleParent?.Family?.FemaleGenotype?.GivenName == string.Empty) ? t.FemaleParent?.Family?.FemaleGenotype?.OriginalName : t.FemaleParent?.Family?.FemaleGenotype?.GivenName,
                FemaleParentMaleParent = (t.FemaleParent?.Family?.MaleGenotype?.GivenName == null || t.FemaleParent?.Family?.MaleGenotype?.GivenName == string.Empty) ? t.FemaleParent?.Family?.MaleGenotype?.OriginalName : t.FemaleParent?.Family?.MaleGenotype?.GivenName,
                MaleParentFemaleParent = (t.MaleParent?.Family?.FemaleGenotype?.GivenName == null || t.MaleParent?.Family?.FemaleGenotype?.GivenName == string.Empty) ? t.MaleParent?.Family?.FemaleGenotype?.OriginalName : t.MaleParent?.Family?.FemaleGenotype?.GivenName,
                MaleParentMaleParent = (t.MaleParent?.Family?.MaleGenotype?.GivenName == null || t.MaleParent?.Family?.MaleGenotype?.GivenName == string.Empty) ? t.MaleParent?.Family?.MaleGenotype?.OriginalName : t.MaleParent?.Family?.MaleGenotype?.GivenName,
                FemaleParentId = t.FemaleParentId,
                MaleParentId = t.MaleParentId,
                GenotypeId = t.GenotypeId,
                GenusId = t.GenusId,
                GenusName = t.Genus.Name,
                Note = t.Note,
                Id = t.Id,
                OriginId = t.OriginId,
                OriginName = t.Origin?.Name,
                Reciprocal = t.Reciprocal,
                Purpose = t.Purpose,
                Year = t.Year,
                FieldPlantedNum = t.FieldPlantedNum,
                SeedNum = t.SeedNum,
                Unsuccessful = t.Unsuccessful,
                DateCreated = t.DateCreated
            }).ToList();
            //var cvm = plans.ToCrossPlanningViewModel();
            return DefaultOrder(viewModels);
        }
        public IEnumerable<CrossPlanViewModel> DefaultOrder(IEnumerable<CrossPlanViewModel> crossPlans)
        {
            return crossPlans.OrderBy(t => t.CrossTypeName).ThenBy(t => t.FemaleParentName).ThenBy(t => t.MaleParentName);
        }

        public IEnumerable<CrossPlanViewModel> OrderByParents(IEnumerable<CrossPlanViewModel> crossPlans)
        {
            return crossPlans.OrderBy(t => t.FemaleParentName).ThenBy(t => t.MaleParentName);
        }

        public CrossPlanViewModel GetCrossPlan(int id)
        {
            CrossPlan crossplan = u_repo.GetCrossPlan(id);
            return crossplan.ToCrossPlanningViewModel();
        }

        public IEnumerable<SelectListItem> GetCrossTypesList(int genusId)
        {
            var items = u_repo.GetCrossTypes().Where(t => t.GenusId == genusId && t.Retired == false);
            return items.ToSelectList(t => t.Id, t => t.Name).OrderBy(t => t.Text);
        }

        public Dictionary<string, int> GetCrossTypeListOrder(IEnumerable<SelectListItem> items)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            List<SelectListItem> itemList = items.ToList();
            for (int i = 0; i < itemList.Count(); i++)
            {
                dict.Add(itemList[i].Value, i);
            }

            return dict;
        }

        private CrossPlanViewModel CreateReciprocal(int id)
        {
            var original = u_repo.GetCrossPlan(id);
            if (original == null)
            {
                throw new CrossPlanException("Invalid Cross Plan");
            }

            var plan = new CrossPlan()
            {
                MaleParent = original.FemaleParent,
                FemaleParent = original.MaleParent,
                Year = original.Year,
                GenusId = original.GenusId,
                CrossTypeId = original.CrossTypeId,
                Purpose = original.Purpose,
                Note = original.Note,
                DateCreated = original.DateCreated,
                Reciprocal = true
            };

            u_repo.SaveCrossPlan(plan);

            return GetCrossPlan(plan.Id);
        }


        public IEnumerable<CrossPlanViewModel> GenerateReciprocals(string year, int genusId)
        {
            var crossplans = u_repo.GetCrossPlans().Where(c => c.GenusId == genusId
                                                            && c.Year == year
                                                            && c.MaleParentId.HasValue
                                                            && c.FemaleParentId.HasValue
                                                            && !c.GenotypeId.HasValue).ToList();
            List<CrossPlanViewModel> newPlans = new List<CrossPlanViewModel>();
            IEnumerable<IList<int>> parentCombos = crossplans.Select(cp => new List<int> { cp.FemaleParentId.Value, cp.MaleParentId.Value }).ToList();
            foreach (var cp in crossplans)
            {
                if (!parentCombos.Any(combo => combo[0].Equals(cp.MaleParentId.Value) && combo[1].Equals(cp.FemaleParentId.Value)))
                {
                    var plan = CreateReciprocal(cp.Id);
                    newPlans.Add(plan);
                }
            }

            if(newPlans.IsNullOrEmpty()){
                throw new CrossPlanException("All reciprocals already created");
            }

            return newPlans;
        }

        public bool CanAddRow(string yearstr, int genusId)
        {
            int year = -1;
            int.TryParse(yearstr, out year);

            bool anyBlankCrossNums = GetCrossPlans(yearstr, genusId)
                                        .Any(c => string.IsNullOrWhiteSpace(c.CrossNum) 
                                                    && (c.MaleParentId.HasValue || c.FemaleParentId.HasValue));
            return year >= DateTime.Now.Year || anyBlankCrossNums;
        }
    }
}
