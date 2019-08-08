using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Data.Models
{
    [Serializable]
    public partial class Fate
    {
        public override string ToString()
        {
            return Name;
        }
    }
}
