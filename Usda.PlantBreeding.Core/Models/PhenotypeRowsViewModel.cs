using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Core.Models
{
    public class PhenotypeRowsViewModel
    {
        public List<string> RowIdList { get; set; }
        public List<MapComponentSummaryVM> MapComponentRows { get; set; }
    }
}
