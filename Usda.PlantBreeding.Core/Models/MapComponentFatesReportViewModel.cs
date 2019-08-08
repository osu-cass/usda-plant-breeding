using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Core.Models
{
    public class MapComponentFatesReportViewModel
    {
        [Display(Name = "Year")]
        [Required]
        public string MapYear { get; set; }

        [Display(Name = "Seedling Map")]
        [Required]
        public bool MapIsSeedling { get; set; }

        [Display(Name = "Genus")]
        public int GenusId { get; set; }

        public string GenusName { get; set; }

    }
}
