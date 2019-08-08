using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Usda.PlantBreeding.Data.Models;

namespace Usda.PlantBreeding.Core.Models
{
    public class MapBuilderViewModel
    {
        public Map Map { get; set; }
        public List<MapComponentViewModel> MapComponentsRow { get; set; }
        public IEnumerable<SelectListItem> Years { get; set; }
        public IEnumerable<SelectListItem> NewYears { get; set; }

        public IEnumerable<int?> Rows { get; set; }

        public MapComponentViewModel NewMapComponent { get; set; }


    }
}
