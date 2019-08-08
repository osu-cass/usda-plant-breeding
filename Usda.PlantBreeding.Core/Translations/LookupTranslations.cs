using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usda.PlantBreeding.Core.Models;
using Usda.PlantBreeding.Data.Models;

namespace Usda.PlantBreeding.Core.Translations
{
    public static class LookupTranslations
    {
        public static QuestionViewModel ToQuestionVM(this Question q)
        {
            return new QuestionViewModel
            {
                Id = q.Id,
                Label = q.Label,
                Text = q.Text,
                Order = q.Order,
                InputType = q.InputType?.Text
            };

        }
        public static FateViewModel ToFateVM(this Fate f)
        {
            return new FateViewModel
            {
                Id = f.Id,
                Name = f.Name,
                Order = f.Order.GetValueOrDefault(),
                Label = f.Label
            };

        }

        public static AnswerViewModel ToAnswerVM(this Answer a)
        {
            return new AnswerViewModel
            {
                Id = a.Id,
                Text = a.Text,
                QuestionId = a.QuestionId,
                GenotypeId = a.GenotypeId,
                MapComponentYearsId = a.MapComponentYearsId
            };
        }

        public static Answer ToAnswer(this AnswerViewModel a)
        {
            return new Answer
            {
                Id = a.Id,
                Text = a.Text,
                QuestionId = a.QuestionId,
                GenotypeId = a.GenotypeId,
                MapComponentYearsId = a.MapComponentYearsId
            };
        }

    }
}
