using AutoMapper;
using System.Linq;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Site.Models;
using Usda.PlantBreeding.Core.Models;

namespace Usda.PlantBreeding.Site.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Genotype, GenotypeUseSummaryViewModel>();
                cfg.CreateMap<MapComponent, MapComponentViewModel>();
                cfg.CreateMap<MapComponentViewModel, MapComponent>();
                cfg.CreateMap<Genotype, AccessionViewModel>();
                cfg.CreateMap<AccessionViewModel, Genotype>();
                cfg.CreateMap<AccessionViewModel, Family>()
                    .ForMember(dt => dt.CrossNum, opts => opts.MapFrom(src => src.FamilyCrossNum))
                    .ForMember(dt => dt.CrossTypeId, opts => opts.MapFrom(src => src.FamilyCrossTypeId))
                    .ForMember(dt => dt.GenusId, opts => opts.MapFrom(src => src.FamilyGenusId))
                    .ForMember(dt => dt.IsReciprocal, opts => opts.MapFrom(src => src.FamilyIsReciprocal))
                    .ForMember(dt => dt.OriginId, opts => opts.MapFrom(src => src.FamilyOriginId))
                    .ForMember(dt => dt.SeedNum, opts => opts.MapFrom(src => src.FamilySeedNum))
                    .ForMember(dt => dt.TransplantedNum, opts => opts.MapFrom(src => src.FamilyTransplantedNum))
                    .ForMember(dt => dt.FieldPlantedNum, opts => opts.MapFrom(src => src.FamilyFieldPlantedNum))
                    .ForMember(dt => dt.Id, opts => opts.MapFrom(src => src.FamilyId))
                    .ForMember(dt => dt.FemaleParent, opts => opts.MapFrom(src => src.FamilyFemaleParent))
                    .ForMember(dt => dt.MaleParent, opts => opts.MapFrom(src => src.FamilyMaleParent))
                    .ForMember(dt => dt.Purpose, opts => opts.MapFrom(src => src.FamilyPurpose));

                cfg.CreateMap<Family, AccessionViewModel>()
                    .ForMember(dt => dt.Id, opts => opts.Ignore())
                    .ForMember(dt => dt.FamilyCrossNum, opts => opts.MapFrom(src => src.CrossNum))
                    .ForMember(dt => dt.FamilyCrossTypeId, opts => opts.MapFrom(src => src.CrossTypeId))
                    .ForMember(dt => dt.FamilyGenusId, opts => opts.MapFrom(src => src.GenusId))
                    .ForMember(dt => dt.FamilyIsReciprocal, opts => opts.MapFrom(src => src.IsReciprocal))
                    .ForMember(dt => dt.FamilyOriginId, opts => opts.MapFrom(src => src.OriginId))
                    .ForMember(dt => dt.FamilyOriginName, opts => opts.MapFrom(src => src.Origin.Name))
                    .ForMember(dt => dt.FamilySeedNum, opts => opts.MapFrom(src => src.SeedNum))
                    .ForMember(dt => dt.FamilyTransplantedNum, opts => opts.MapFrom(src => src.TransplantedNum))
                    .ForMember(dt => dt.FamilyFieldPlantedNum, opts => opts.MapFrom(src => src.FieldPlantedNum))
                    .ForMember(dt => dt.FamilyId, opts => opts.MapFrom(src => src.Id))
                    .ForMember(dt => dt.FamilyFemaleParent, opts => opts.MapFrom(src => src.FemaleParent))
                    .ForMember(dt => dt.FamilyMaleParent, opts => opts.MapFrom(src => src.MaleParent))
                    .ForMember(dt => dt.FamilyFemaleGenotypeName, opts => opts.MapFrom(src => src.FemaleGenotype.Name))
                    .ForMember(dt => dt.FamilyMaleGenotypeName, opts => opts.MapFrom(src => src.MaleGenotype.Name))
                     .ForMember(dt => dt.FamilyPurpose, opts => opts.MapFrom(src => src.Purpose));

                cfg.CreateMap<Genotype, AccessionIndexViewModel>();
                cfg.CreateMap<MapQuestionViewModel, Question>();
                cfg.CreateMap<Question, MapQuestionViewModel>();
                cfg.CreateMap<CrossPlan, CrossPlanViewModel>()
                   .ForMember(dt => dt.MaleParentMaleParent, opts => opts.MapFrom(src => src.MaleParent.Family.MaleGenotype.Name))
                   .ForMember(dt => dt.MaleParentFemaleParent, opts => opts.MapFrom(src => src.MaleParent.Family.FemaleGenotype.Name))
                   .ForMember(dt => dt.FemaleParentMaleParent, opts => opts.MapFrom(src => src.FemaleParent.Family.MaleGenotype.Name))
                   .ForMember(dt => dt.FemaleParentFemaleParent, opts => opts.MapFrom(src => src.FemaleParent.Family.FemaleGenotype.Name));
                cfg.CreateMap<CrossPlanViewModel, CrossPlan>();
                cfg.CreateMap<CrossPlanViewModel, CrossPlanViewModel>();
                cfg.CreateMap<MapComponentYears, MapComponentSummaryViewModel>()
                  .ForMember(dt => dt.Answers, opts => opts.MapFrom(src => src.Answers.Distinct().ToDictionary(a => a.QuestionId, a => a)));
            });
        }

        //OBSOLETE CODE
        /*private static void GenotypeUseSummaryViewModelMapping()
        {
            Mapper.CreateMap<Genotype, GenotypeUseSummaryViewModel>();
        }

        private static void MapBuilderMapping()
        {
            Mapper.CreateMap<MapComponent, MapComponentViewModel>();
            Mapper.CreateMap<MapComponentViewModel, MapComponent>();

        }

        private static void AccessionViewModelMapping()
        {
            Mapper.CreateMap<Genotype, AccessionViewModel>();
            Mapper.CreateMap<AccessionViewModel, Genotype>();
            Mapper.CreateMap<AccessionViewModel, Family>()
                .ForMember(dt => dt.CrossNum, opts => opts.MapFrom(src => src.FamilyCrossNum))
                .ForMember(dt => dt.CrossTypeId, opts => opts.MapFrom(src => src.FamilyCrossTypeId))
                .ForMember(dt => dt.GenusId, opts => opts.MapFrom(src => src.FamilyGenusId))
                .ForMember(dt => dt.IsReciprocal, opts => opts.MapFrom(src => src.FamilyIsReciprocal))
                .ForMember(dt => dt.OriginId, opts => opts.MapFrom(src => src.FamilyOriginId))
                .ForMember(dt => dt.SeedNum, opts => opts.MapFrom(src => src.FamilySeedNum))
                .ForMember(dt => dt.TransplantedNum, opts => opts.MapFrom(src => src.FamilyTransplantedNum))
                .ForMember(dt => dt.FieldPlantedNum, opts => opts.MapFrom(src => src.FamilyFieldPlantedNum))
                .ForMember(dt => dt.Id, opts => opts.MapFrom(src => src.FamilyId))
                .ForMember(dt => dt.FemaleParent, opts => opts.MapFrom(src => src.FamilyFemaleParent))
                .ForMember(dt => dt.MaleParent, opts => opts.MapFrom(src => src.FamilyMaleParent))
                .ForMember(dt => dt.Purpose, opts => opts.MapFrom(src => src.FamilyPurpose));

            Mapper.CreateMap<Family, AccessionViewModel>()
                .ForMember(dt => dt.Id, opts => opts.Ignore())
                .ForMember(dt => dt.FamilyCrossNum, opts => opts.MapFrom(src => src.CrossNum))
                .ForMember(dt => dt.FamilyCrossTypeId, opts => opts.MapFrom(src => src.CrossTypeId))
                .ForMember(dt => dt.FamilyGenusId, opts => opts.MapFrom(src => src.GenusId))
                .ForMember(dt => dt.FamilyIsReciprocal, opts => opts.MapFrom(src => src.IsReciprocal))
                .ForMember(dt => dt.FamilyOriginId, opts => opts.MapFrom(src => src.OriginId))
                .ForMember(dt => dt.FamilyOriginName, opts => opts.MapFrom(src => src.Origin.Name))
                .ForMember(dt => dt.FamilySeedNum, opts => opts.MapFrom(src => src.SeedNum))
                .ForMember(dt => dt.FamilyTransplantedNum, opts => opts.MapFrom(src => src.TransplantedNum))
                .ForMember(dt => dt.FamilyFieldPlantedNum, opts => opts.MapFrom(src => src.FieldPlantedNum))
                .ForMember(dt => dt.FamilyId, opts => opts.MapFrom(src => src.Id))
                .ForMember(dt => dt.FamilyFemaleParent, opts => opts.MapFrom(src => src.FemaleParent))
                .ForMember(dt => dt.FamilyMaleParent, opts => opts.MapFrom(src => src.MaleParent))
                .ForMember(dt => dt.FamilyFemaleGenotypeName, opts => opts.MapFrom(src => src.FemaleGenotype.Name))
                .ForMember(dt => dt.FamilyMaleGenotypeName, opts => opts.MapFrom(src => src.MaleGenotype.Name))
                 .ForMember(dt => dt.FamilyPurpose, opts => opts.MapFrom(src => src.Purpose));

            Mapper.CreateMap<Genotype, AccessionIndexViewModel>();
        }

        private static void MapQuestionViewModelMapping()
        {
            Mapper.CreateMap<MapQuestionViewModel, Question>();
            Mapper.CreateMap<Question, MapQuestionViewModel>();
        }

        public static void CrossPlanningViewModelMapping()
        {
            Mapper.CreateMap<CrossPlan, CrossPlanViewModel>()
                .ForMember(dt => dt.MaleParentMaleParent, opts => opts.MapFrom(src => src.MaleParent.Family.MaleGenotype.Name))
                .ForMember(dt => dt.MaleParentFemaleParent, opts => opts.MapFrom(src => src.MaleParent.Family.FemaleGenotype.Name))
                .ForMember(dt => dt.FemaleParentMaleParent, opts => opts.MapFrom(src => src.FemaleParent.Family.MaleGenotype.Name))
                .ForMember(dt => dt.FemaleParentFemaleParent, opts => opts.MapFrom(src => src.FemaleParent.Family.FemaleGenotype.Name));
            Mapper.CreateMap<CrossPlanViewModel, CrossPlan>();
            Mapper.CreateMap<CrossPlanViewModel, CrossPlanViewModel>();

        }

        public static void GenotypeSummaryViewModelMapping()
        {
            Mapper.CreateMap<MapComponentYears, MapComponentSummaryViewModel>()
                  .ForMember(dt => dt.Answers, opts => opts.MapFrom(src => src.Answers.Distinct().ToDictionary(a => a.QuestionId, a => a)));
        }*/      
    }
}