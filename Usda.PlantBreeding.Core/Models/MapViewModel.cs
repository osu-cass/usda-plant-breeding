using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Core.Models
{
    public class MapViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PlantingYear { get; set; }
        public string PicklistPrefix { get; set; }
        public string LocationName { get; set; }
        public string LocationAddress { get; set; }
        public string LocationSuffix { get; set; }
        public string CrossTypeName { get; set; }
        public string DefaultPlant { get; set; }
        public string Note { get; set; }
        public string StartCorner { get; set; }
        public string EvaluationYear { get; set; }
    }
}
