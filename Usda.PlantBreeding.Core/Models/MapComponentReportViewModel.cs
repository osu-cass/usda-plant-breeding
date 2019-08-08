using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Usda.PlantBreeding.Core.Attributes;

namespace Usda.PlantBreeding.Core.Models
{
    public class MapComponentReportViewModel : MapViewReportViewModel
    {
        
        [ReportParameterAttribute("Virus1Label")]
        public string Virus1Label { get; set; }
        
        [ReportParameterAttribute("Virus2Label")]
        public string Virus2Label { get; set; }
        
        [ReportParameterAttribute("Virus3Label")]
        public string Virus3Label { get; set; }
        
        [ReportParameterAttribute("Virus4Label")]
        public string Virus4Label { get; set; }

    }
}
