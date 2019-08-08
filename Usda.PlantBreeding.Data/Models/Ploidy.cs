using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Data.Models
{
    [MetadataType(typeof(PloidyMetadata))]
    [DisplayColumn("Name")]
    [DisplayName("Ploidy")]
    public partial class Ploidy
    {
        public override string ToString()
        {
            return Name;
        }
    }

    public class PloidyMetadata
    {
        [Required]
        [Display(Name = "Code")]
        public string Name { get; set; }

        [Display(Name = "Value")]
        public string Value { get; set; }
    }
}
