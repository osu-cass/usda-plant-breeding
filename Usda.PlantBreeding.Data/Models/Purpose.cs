using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Data.Models
{
    [MetadataType(typeof(PurposeMetaData))]
    public partial class Purpose
    {
        public override string ToString()
        {
            return Name;
        }
    }

    public class PurposeMetaData
    {
        [Required]
        [Display(Name="Name")]
        public string Name { get; set; }

        [Display(Name = "Value")]
        public string Value { get; set; }

    }
}
