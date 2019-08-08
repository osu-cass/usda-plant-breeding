using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Usda.PlantBreeding.Core.Models
{
    public class CrossPlanIndexViewModel
    {
        public List<CrossPlanViewModel> CrossPlans { get; set; }

        public CrossPlanViewModel NewCrossPlan { get; set; }
        public IEnumerable<SelectListItem> Years { get; set; }
        public IEnumerable<SelectListItem> CrossTypes { get; set; }

        public int GenusId { get; set; }

        [Display(Name = "Default Origin")]
        public string DefaultOriginName { get; set; }
        
        [Display(Name = "Next Cross")]
        public string NextCrossNumber { get; set; }

        public string CurrentYear { get; set; }

        public Dictionary<string, int> CrossTypesOrder { get; set; }
    }
}