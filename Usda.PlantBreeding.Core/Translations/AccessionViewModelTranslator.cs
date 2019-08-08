using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Usda.PlantBreeding.Core.Models;
using Usda.PlantBreeding.Data.Models;

namespace Usda.PlantBreeding.Core.Translations
{
    public static class AccessionViewModelTranslator
    {
        public static AccessionViewModel ToAccessionViewModel(this Genotype genotype)
        {
            return Mapper.Map<Genotype, AccessionViewModel>(genotype);
        }

        public static Genotype ToGenotype(this AccessionViewModel accession)
        {
            return Mapper.Map<AccessionViewModel, Genotype>(accession);
        }


        public static Family ToFamily(this AccessionViewModel accession)
        {
            return Mapper.Map<AccessionViewModel, Family>(accession);
        }

        public static IEnumerable<AccessionIndexViewModel> ToAccessionIndexViewModel(this IEnumerable<Genotype> genotypes)
        {
            return Mapper.Map<IEnumerable<Genotype>, IEnumerable<AccessionIndexViewModel>>(genotypes);
        }

        public static void CopyCrossToAccession(this AccessionViewModel accession, CrossPlan cross)
        {
            if (cross.MaleParentId.HasValue)
            {
                accession.FamilyMaleParent = cross.MaleParentId;
                accession.FamilyMaleGenotypeName = cross.MaleParent.Name;
            }
            if (cross.FemaleParentId.HasValue)
            {
                accession.FamilyFemaleParent = cross.FemaleParentId;
                accession.FamilyFemaleGenotypeName = cross.FemaleParent.Name;


            }
            accession.Year = cross.Year;
            // accession.IsPopulation = //TODO: add this?
            accession.FamilyBaseGenotypeYear = cross.Year;
            accession.FamilyGenusId = cross.GenusId;
            accession.FamilyTransplantedNum = cross.TransplantedNum;
            accession.FamilySeedNum = cross.SeedNum;
            accession.FamilyFieldPlantedNum = cross.FieldPlantedNum;
            accession.FamilyCrossNum = cross.CrossNum;
            accession.FamilyBaseGenotypeNote = cross.Note;
            accession.FamilyUnsuccessful = cross.Unsuccessful;
            accession.FamilyCrossTypeId = cross.CrossTypeId;
            accession.CrossPlanId = cross.Id;
            accession.FamilyPurpose = cross.Purpose;

        }

        public static void UpdateFromCrossPlan(this Genotype dest, CrossPlan crossplan)
        {
            dest.Note = crossplan.Note;
            dest.Family.Purpose = crossplan.Purpose;
            dest.Family.SeedNum = crossplan.SeedNum;
            dest.Family.Unsuccessful = crossplan.Unsuccessful;
            dest.Family.FieldPlantedNum = crossplan.FieldPlantedNum;
            dest.Family.TransplantedNum = crossplan.TransplantedNum;

        }

        public static GenotypeViewModel ToGenotypeVM(this Genotype g)
        {

            return new GenotypeViewModel
            {
                Id = g.Id,
                Name = g.Name,
                FemaleParent = g.Family?.FemaleGenotype?.Name,
                MaleParent = g.Family?.MaleGenotype?.Name,
                CreatedDate = g.CreatedDate ?? DateTime.MinValue,
                CrossTypeName = g.Family?.CrossType?.Name,
                FamilyName = g.Family.Origin + g.Family.CrossNum,
                Selection = g.SelectionNum,
                IsPopulation = g.IsPopulation
            };
        }
    }
}