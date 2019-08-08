using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Core.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class ReportParameterAttribute : Attribute
    {
        private string paramName;

        public ReportParameterAttribute(string itemName)
        {
            this.paramName = itemName;

        }
        public virtual string ParameterName
        {
            get { return paramName; }
        }

    }
}
