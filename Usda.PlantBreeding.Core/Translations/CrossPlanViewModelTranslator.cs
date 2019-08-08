using AutoMapper;
using System;
using System.Collections.Generic;
using Usda.PlantBreeding.Core.Models;
using Usda.PlantBreeding.Data.Models;


namespace Usda.PlantBreeding.Core.Translations
{
    public static class CrossPlanViewModelTranslator
    {
        public static IEnumerable<CrossPlanViewModel> ToCrossPlanningViewModel(this IEnumerable<CrossPlan> crossPlans)
        {
            return Mapper.Map<IEnumerable<CrossPlan>, IEnumerable<CrossPlanViewModel>>(crossPlans);
        }
        public static IEnumerable<CrossPlan> ToCrossPlan(this IEnumerable<CrossPlanViewModel> crossPlanIndex)
        {
            return Mapper.Map<IEnumerable<CrossPlanViewModel>, IEnumerable<CrossPlan>>(crossPlanIndex);
        }

        public static CrossPlanViewModel ToCrossPlanningViewModel(this CrossPlan crossPlan)
        {
            return Mapper.Map<CrossPlan, CrossPlanViewModel>(crossPlan);
        }

        public static CrossPlan ToCrossPlan(this CrossPlanViewModel cpvm) 
        {
            return Mapper.Map<CrossPlanViewModel, CrossPlan>(cpvm);
        }

        public static CrossPlanViewModel FemaleGenotypeToCrossPlan(this CrossPlanViewModel cpvm, Genotype female)
        {
            if(female == null)
            {
                throw new System.ArgumentException("Parameter cannot be null", "Genotype female");
            }
            
            if (female.Family.FemaleParent.HasValue)
            {
                cpvm.FemaleParentFemaleParent = female.Family.FemaleGenotype.Name;
            }

            if (female.Family.MaleParent.HasValue)
            {
                cpvm.FemaleParentMaleParent = female.Family.MaleGenotype.Name;
            }
            return cpvm;
        
        }

        public static CrossPlanViewModel MaleGenotypeToCrossPlan(this CrossPlanViewModel cpvm, Genotype male)
        {
            if (male == null)
            {
                throw new System.ArgumentException("Parameter cannot be null", "Genotype male");
            }

            if (male.Family.FemaleParent.HasValue)
            {
                cpvm.MaleParentFemaleParent = male.Family.FemaleGenotype.Name;
            }

            if (male.Family.MaleParent.HasValue)
            {
                cpvm.MaleParentMaleParent = male.Family.MaleGenotype.Name;
            }
            return cpvm;

        }
        public static void UpdateFromAccession(this CrossPlan dest, Genotype genotype)
        {
            dest.Note = genotype.Note;
            dest.Purpose = genotype.Family.Purpose;
            dest.SeedNum = genotype.Family.SeedNum;
            dest.Unsuccessful = genotype.Family.Unsuccessful;
            dest.CrossTypeId = genotype.Family.CrossTypeId;
            dest.CrossType = genotype.Family.CrossType;
            dest.CrossNum = genotype.Family.CrossNum;
            dest.GenotypeId = genotype.Id;
            dest.FieldPlantedNum = genotype.Family.FieldPlantedNum;
            dest.TransplantedNum = genotype.Family.TransplantedNum;

        }

        public static void CopyCrossPlan(this CrossPlan dest, CrossPlan src)
        {
            dest.FemaleParentId = src.FemaleParentId;
            dest.MaleParentId = src.MaleParentId;
            dest.Note = src.Note;
            dest.Purpose = src.Purpose;
            dest.CrossTypeId = src.CrossTypeId;
            dest.Reciprocal = src.Reciprocal;
            dest.Unsuccessful = src.Unsuccessful;
            dest.DateCreated = src.DateCreated;
        
        }

        public static void CopyCrossPlanViewModel(this CrossPlan dest, CrossPlanViewModel src)
        {
            dest.FemaleParentId = src.FemaleParentId;
            dest.MaleParentId = src.MaleParentId;
            dest.CrossTypeId = src.CrossTypeId;
            dest.Reciprocal = src.Reciprocal;
            dest.Unsuccessful = src.Unsuccessful;
            dest.DateCreated = src.DateCreated;
            dest.Note = src.Note;
            dest.CrossNum = src.CrossNum;
            dest.SeedNum = src.SeedNum;
            dest.TransplantedNum = src.TransplantedNum;
            dest.FieldPlantedNum = src.FieldPlantedNum;
            dest.Purpose = src.Purpose;
        }


    }
}