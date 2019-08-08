using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Core.Models
{
    public class MapInventoryReportViewModel
    {
        [Required]
        [Display(Name = "Evaluation Year")]
        public string MapYear { get; set; }
        [Display(Name = "Map Name")]
        [Required]
        public string MapName { get; set; }
        [Display(Name = "Map Est. Year")]
        public string MapPlantedYear { get; set; }
        [Display(Name = "Map Id")]
        [Required]
        public int MapId { get; set; }

        [Display(Name = "Seedling Map?")]

        public bool MapIsSeedling { get; set; }

        public string MapFullName { get; set; }
    }
}
