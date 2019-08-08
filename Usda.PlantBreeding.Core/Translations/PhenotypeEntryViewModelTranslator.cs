using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Core.Models;
using System;

namespace Usda.PlantBreeding.Core.Translations
{
    public static class PhenotypeEntryViewModelTranslator
    {
        public static MapComponentSummaryViewModel ToMapComponentSummaryViewModel(this MapComponentYears mapComponent)
        {
            MapComponentSummaryViewModel viewmodel = Mapper.Map<MapComponentYears, MapComponentSummaryViewModel>(mapComponent);
            ICollection<Question> unanswered = mapComponent.MapComponent.Map.Questions;
            viewmodel.Answers = GenerateRowsFullAnswers(mapComponent.Id, viewmodel.MapComponentGenotypeId, unanswered, null);
            return viewmodel;
        }

        public static IDictionary<int, Answer> GenerateRowsFullAnswers(int rowId, int genotypeId, IEnumerable<Question> unanswered, IDictionary<int, Answer> answers)
        {
            if (answers == null)
            {
                answers = new Dictionary<int, Answer>();
            }
            Dictionary<int, Answer> result = answers
                .Union(unanswered.ToDictionary(q => q.Id, q => new Answer() { Id = 0, GenotypeId = genotypeId, MapComponentYearsId = rowId, QuestionId = q.Id }))
                .ToDictionary(t => t.Key, t => t.Value);
            return result;
        }

        public static IDictionary<string, AnswerViewModel> GenerateRowsFullAnswerVMs(int rowId, int genotypeId, int mapComponentYearsId,  IEnumerable<Question> unanswered, IDictionary<string, AnswerViewModel> answers)
        {
            if (answers == null)
            {
                answers = new Dictionary<string, AnswerViewModel>();
            }
            Dictionary<string, AnswerViewModel> result = answers
                .Union(unanswered.ToDictionary(q => q.Id.ToString(), q => new AnswerViewModel() { Id = -1, GenotypeId = genotypeId, QuestionId = q.Id, MapComponentYearsId = mapComponentYearsId }))
                .ToDictionary(t => t.Key, t => t.Value);
            return result;
        }
    

        public static MapComponentSummaryVM ToMapComponentSummaryVM(this MapComponentYears mc)
        {
            return new MapComponentSummaryVM
            {
                Id = mc.Id,
                Genotype = mc.MapComponent?.Genotype?.ToGenotypeVM(),
                PickingNumber = mc.MapComponent?.PickingNumber,
                Row = mc.MapComponent?.Row,
                PlantNum = mc.MapComponent?.PlantNum,
                Comments = mc.Comments,
                Rep = mc.MapComponent?.Rep,
                Fates = mc.Fates?.Select(t => t.ToFateVM()),
                QuestionToAnswer = mc.Answers?.DistinctBy(t => t.QuestionId.ToString())
                    .ToDictionary(t => t.QuestionId.ToString(), t => t.ToAnswerVM()),
                MapName = mc.MapComponent.Map.FullName,
                EvalYear = mc.Year.Year ?? "",
                MapSeedling = mc.MapComponent.Map.IsSeedlingMap
            };
        }
        public static MapComponentSummaryVM ToMapComponentSummaryVM(this MapComponentYears mc, MapComponentYears previous)
        {

            var mapComponentYear = mc.ToMapComponentSummaryVM();
            if(previous != null && previous.Fates.Any())
            {
                List<string> fates = previous.Fates.Select(f => f.Label).ToList();
                mapComponentYear.PreviousFates = string.Join("; ", fates);
            }

            return mapComponentYear;
        }

        private static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}