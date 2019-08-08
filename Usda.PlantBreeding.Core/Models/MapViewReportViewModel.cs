using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Usda.PlantBreeding.Core.Models
{
    public class MapViewReportViewModel
    {
        [Display(Name = "Evaluation Year")]
        [Required]
        public string MapYear { get; set; }
        [Display(Name = "Map Name")]
        [Required]
        public string MapName { get; set; }
        [Display(Name = "Map Planted Year")]
        public string MapPlantedYear { get; set; }
        [Display(Name = "Map Id")]
        [Required]
        public int MapId { get; set; }
        [Display(Name = "Print Picking?")]
        [Required]
        public bool PrintPicking { get; set; }
        public IEnumerable<SelectListItem> SeedlingComponentTypes { get; set; }

        public bool MapIsSeedling { get; set; }
        
        [Display(Name = "Options")]
        public string SeedlingComponentValue { get; set; }
        public string MapFullName { get; set; }



        public MapViewReportViewModel()
        {
            this.SeedlingComponentTypes = new List<SelectListItem>()
                                        {
                                            new SelectListItem{Text = "Both", Value = "Both"}, 
                                            new SelectListItem{Text = "Selections Made", Value = "SelectionsMade"}, 
                                            new SelectListItem{Text = "Planted", Value = "Planted"}
                                        };
        }


     
    }
}
