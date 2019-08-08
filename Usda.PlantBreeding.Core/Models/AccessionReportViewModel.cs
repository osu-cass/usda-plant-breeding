using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Usda.PlantBreeding.Core.Models
{
    public class AccessionReportViewModel
    {
        [Display(Name = "Year")]
        public string Year { get; set; }

        public bool isPlanning { get; set; }

        [Display(Name = "Genus")]
        public string GenusName { get; set; }
        public string GenusValue { get; set; }
        
        [Display(Name = "Genus")]
        public int GenusId { get; set; }
        
        [Display(Name = "Origin")]
        public string OriginOptionValue { get; set; }
        public IEnumerable<SelectListItem> OriginOptions { get; set; }
        [Display(Name = "Accession Type")]
        public string AccessionOptionValue { get; set; }
        public IEnumerable<SelectListItem> AccessionReportOptions { get; set; }
        public AccessionReportViewModel()
        {
            this.AccessionReportOptions = new List<SelectListItem>()
                                        {
                                            new SelectListItem{Text = "Both", Value = "Both"}, 
                                            new SelectListItem{Text = "Selections", Value = "Selections"}, 
                                            new SelectListItem{Text = "Populations", Value = "Populations"}
                                        };
            this.OriginOptions = new List<SelectListItem>()
                                        {
                                            new SelectListItem{Text = "Both", Value = "Both"}, 
                                            new SelectListItem{Text = "External", Value = "External"}, 
                                            new SelectListItem{Text = "Internal", Value = "Internal"}
                                        };
        }
    }
}
