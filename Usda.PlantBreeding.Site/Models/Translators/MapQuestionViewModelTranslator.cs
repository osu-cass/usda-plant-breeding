using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Usda.PlantBreeding.Core.Models;
using Usda.PlantBreeding.Data.Models;

namespace Usda.PlantBreeding.Site.Models.Translators
{
    public static class MapQuestionViewModelTranslator
    {
        /// <summary>
        /// Creates a view model for the selection of questions on map creation
        /// </summary>
        /// <param name="questions">The list of questions associated with the genus</param>
        /// <param name="genusId">The genus that is set as state</param>
        /// <returns>Loaded MapQuestionListViewModel</returns>
        public static MapQuestionListViewModel ToMapQuestionListViewModel(this List<Question> availQuestions, Genus genus, int defaultPlantsInRep)
        {
            if (availQuestions == null)
            {
                throw new ArgumentNullException("questions");
            }
            Map map = new Map()
            {
                GenusId = genus.Id,
                Genus = genus,
                PlantingYear = DateTime.Now.Year.ToString(),
                DefaultPlantsInRep = defaultPlantsInRep,
            };
            return map.ToMapQuestionListViewModel(availQuestions);
        }

        /// <summary
        /// This returns a mapquestionlistviewmodel that intersects a list of available questions to the list of questions on the map
        /// </summary>
        /// <param name="map"></param>
        /// <param name="availQuestions"></param>
        /// <returns></returns>
        public static MapQuestionListViewModel ToMapQuestionListViewModel(this Map map, List<Question> availQuestions)
        {
            if (availQuestions == null)
            {
                throw new ArgumentNullException("questions");
            }
            List<MapQuestionViewModel> selectedQuestions = map.Questions.ToMapQuestionViewModel().ToList();
            foreach (var item in selectedQuestions)
            {
                item.isChecked = true;
            }
            List<MapQuestionViewModel> availQuestionsVM = availQuestions.ToMapQuestionViewModel().ToList();
            List<MapQuestionViewModel> allQuestions = selectedQuestions
                .Union(availQuestionsVM, new MapQuestionViewModelComparer())
                .OrderBy(q => q.Order)
                .ToList();
            return new MapQuestionListViewModel()
            {
                Map = map,
                MapId = map.Id,
                Questions = allQuestions
            };
        }

        public static IEnumerable<MapQuestionViewModel> ToMapQuestionViewModel(this IEnumerable<Question> questions)
        {
            return Mapper.Map<IEnumerable<Question>, IEnumerable<MapQuestionViewModel>>(questions);
        }
        public static IEnumerable<Question> ToQuestion(this IEnumerable<MapQuestionViewModel> mapQuestions)
        {
            return Mapper.Map<IEnumerable<MapQuestionViewModel>, IEnumerable<Question>>(mapQuestions);
        }
    }
}