using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Core.Models
{
    public class GenotypeUseSummaryViewModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Original Name")]
        public string OriginalName { get; set; }
        public int Id { get; set; }
         [Display(Name = "Field #")]
        public int? FieldPlantedNum { get; set; }
        
        [Display(Name = "Trans. #")]
        public int? TransplantedNum { get; set; }

        [Display(Name = "Year")]
        public string Year { get; set; }
    }
}
