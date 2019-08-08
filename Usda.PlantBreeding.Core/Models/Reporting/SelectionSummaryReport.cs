using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Core.Models.Reporting
{
    public class SelectionSummaryReport
    {
        [Required]
        public int GenotypeId { get; set; }

        public string GenotypeName { get; set; }
    }
}
