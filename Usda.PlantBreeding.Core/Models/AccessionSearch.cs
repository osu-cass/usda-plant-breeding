using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Core.Models
{
    public class AccessionSearch
    {
        public int GenotypeId { get; set; }
        public string GivenName { get; set; }

        public string OriginalName { get; set; }
        public string Alias1 { get; set; }
        public string Alias2 { get; set; }

    }
}
