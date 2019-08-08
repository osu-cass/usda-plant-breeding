using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Data.Models
{
    [MetadataType(typeof(LocationMetaData))]
    [DisplayColumn("Name")]
    public partial class Location
    {
        public override string ToString()
        {
            return Name;
        }
    }

    public class LocationMetaData
    {
        [Required]
        public string Name { get; set; }
        [Display(Name="Code")]
        public string Description { get; set; }

    }
}
