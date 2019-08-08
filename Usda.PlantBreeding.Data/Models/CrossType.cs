using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using BSGUtils;
using System;
namespace Usda.PlantBreeding.Data.Models
{
    [MetadataType(typeof(CrossTypeMetadata))]
    [DisplayColumn("Name")]
    [DisplayName("Cross Type")]
    public partial class CrossType
    {
        public string NameLabel
        {
            get {
                return "Name";
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
 
    public class CrossTypeMetadata
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name="Genus")]
        public int GenusId { get; set; }
        [Display(Name="Value")]
        public string Value { get; set; }
        public bool Retired { get; set; }
    }
}
