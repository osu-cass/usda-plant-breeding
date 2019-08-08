using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Data.Models
{
    [MetadataType(typeof(YearsMetaData))]
    [DisplayColumn("Year")]
    public partial class Years
    {
        public override string ToString()
        {
            return this.Year;
        }
    }

    public class YearsMetaData
    {
        public int Id { get; set; }
        public int MapId { get; set; }

        [Display(Name="Evaluation Year")]
        public string Year { get; set; }
    
    }
}
