using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usda.PlantBreeding.Data.Models;

namespace Usda.PlantBreeding.Core.Models
{
    public class OrdersEditViewModel
    {
        public OrderViewModel Order { get; set; }

        public List<Material> Materials { get; set; }
        public int OrderId { get; set; }
    }
}
