using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Core.Models
{
    public class BreedingSummaryReportViewModel
    {
        [Display(Name="Genus")]
        public string GenusName { get; set; }
        [Display(Name = "Genus")]
        public int GenusId { get; set; }
        public string GenusValue { get; set; }
    }
}
