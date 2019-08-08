using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Reporting.WebForms;


namespace Usda.PlantBreeding.Core.Models
{
    public class ReportViewModel
    {
        public List<ReportParameter> parameters { get; set; }
        public string ReportName { get; set; }

        public string ReportTitle { get; set; }

    }
}
