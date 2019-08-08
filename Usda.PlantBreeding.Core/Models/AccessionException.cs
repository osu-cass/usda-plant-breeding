using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Core.Models
{
    public class AccessionException: Exception
    {
        public AccessionException(string message)
            : base(message)
        {

        }
    }
}
