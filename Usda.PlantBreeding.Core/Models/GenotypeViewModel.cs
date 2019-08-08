using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Core.Models
{
    public class GenotypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FemaleParent { get; set; }
        public string MaleParent { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CrossTypeName { get; set; }
        public string FamilyName { get; set; }
        public int? Selection { get; set; }

        public bool IsPopulation { get; set; }
    }
}
