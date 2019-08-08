using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Core.Models
{
    public class PhenotypeReportViewModel
    {
        [Display(Name = "Evaluation Year")]
        [Required]
        public string MapYear { get; set; }
        [Display(Name = "Map Planted Year")]
        public string MapPlantedYear { get; set; }
        [Display(Name = "Map Name")]
        [Required]
        public string MapName { get; set; }
        [Display(Name = "Map Id")]
        [Required]
        public int MapId { get; set; }
        public string MapFullName { get; set; }
    }
}
