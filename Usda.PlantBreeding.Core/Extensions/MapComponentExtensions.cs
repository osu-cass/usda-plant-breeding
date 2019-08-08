using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usda.PlantBreeding.Data.Models;

namespace Usda.PlantBreeding.Core.Extensions
{
    public static class MapComponentExtensions
    {
        public static string GetPickingNumber(this MapComponent mc, string year)
        {
            string val = string.Empty;
            string shortYear = "";
            if (year.Length == 4)
            {
                shortYear = year.Substring(2);
            }

            if (mc.ExternalId.HasValue)
            {
                val = mc.ExternalId.ToString();
            }
            else if(mc.MapId > 0  && mc.Map != null)
            {
                val = string.Format("{0}R{1}P{2}", shortYear, mc.Row, mc.PlantNum);
            }
            
            return val;
        }
        

    }
}
