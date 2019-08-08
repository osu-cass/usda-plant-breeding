using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Usda.PlantBreeding.Data.Models;

namespace Usda.PlantBreeding.Core.Models
{
    public class MapComponentSummaryVM
    {
        public int Id { get; set; }
        public string PickingNumber { get; set; }
        public int? PlantNum { get; set; }
        public int? Row { get; set; }
        public string Comments { get; set; }
        public int? Rep { get; set; }
        public string PreviousFates { get; set; }
        public string MapName { get; set; }
        public string EvalYear { get; set; }
        public bool MapSeedling { get; set; }
        public GenotypeViewModel Genotype {get;set;}
        public IEnumerable<FateViewModel> Fates { get; set; }
        public IDictionary<string, AnswerViewModel> QuestionToAnswer { get; set; }
    }
}