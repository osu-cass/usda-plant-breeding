using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Core.Models
{
    public class CrossPlanReportViewModel
    {
        [Display(Name = "Genus")]
        [Required]
        public int? GenusId { get; set; }
        [Display(Name = "Genus")]
        public string GenusName { get; set; }
        public string GenusValue { get; set; }
        [Display(Name = "Year")]
        [Required]
        public string Year { get; set; }
       
    }
}
