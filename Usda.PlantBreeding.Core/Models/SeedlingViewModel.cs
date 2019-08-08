using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Core.Models
{
    public class SeedlingViewModel
    {
        public int MapId {get; set;}
        public int GenusId { get; set; }
        public int MapYearId { get; set; }
        public int GenotypeId { get; set; }
        public string GenotypeName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PlantNum { get; set; }
        public int RowNum { get; set; }

    }
}
