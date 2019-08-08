using System.Collections.Generic;
using Usda.PlantBreeding.Data.Models;
using System.Linq;
using System.Web.Mvc;

namespace Usda.PlantBreeding.Core.Models
{
    public class PhenotypeEntryViewModel
    {
        public string Type { get; set; }
        public int YearId { get; set; }
        public string YearName { get; set; }
        public int GenusId { get; set; }
        public MapViewModel Map { get; set; }
        public List<string> QuestionOrder { get; set; }
        public IDictionary<string, QuestionViewModel> QuestionToQuestion { get; set; }
        public GenotypeViewModel Genotype { get; set; }
        public List<FateViewModel> Fates { get; set; }
    }
}