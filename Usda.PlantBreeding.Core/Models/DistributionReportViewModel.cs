using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Core.Models
{
    public class DistributionReportViewModel
    {
        [Display(Name = "Year")]
        public string Year { get; set; }
        [Display(Name = "Location Id")]
        public int? LocationId { get; set; }
        [Display(Name = "Genotype Id")]
        public int? GenotypeId { get; set; }
    }
}
