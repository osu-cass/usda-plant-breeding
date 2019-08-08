using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Data.Models
{
    [MetadataType(typeof(GenusMetadata))]
    [DisplayColumn("Value"), DisplayName("Genus")]
    public partial class Genus
    {
        public string NameLabel
        {
            get
            {
                return "*Name";
            }
        }

        public string ValueLabel
        {
            get
            {
                return "*Value";
            }
        }

        public string DefaultPlantsInRepLabel
        {
            get
            {
                return "*Default Num of Plants:";
            }
        }
        public override string ToString()
        {
            return Value;
        }
    }

    public class GenusMetadata
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        [Display(Name="Default Num of Plants")]
        public int DefaultPlantsInRep { get; set; }

        [Display(Name="Virus 1")]
        public string VirusLabel1 { get; set; }

        [Display(Name = "Virus 2")]
        public string VirusLabel2 { get; set; }

        [Display(Name = "Virus 3")]
        public string VirusLabel3 { get; set; }

        [Display(Name = "Virus 4")]
        public string VirusLabel4 { get; set; }
    }
}
