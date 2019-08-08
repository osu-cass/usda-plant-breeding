using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using BSGUtils;
using System;

namespace Usda.PlantBreeding.Data.Models
{
    [MetadataType(typeof(FamilyMetaData))]
    [DisplayColumn("Name")]
    [DisplayName("Family")]
    public partial class Family
    {
        public string Name { get { return this.OriginalName; } }

        public string OriginalName
        {
            get
            {
                string val = "";
                if (this.OriginId != null && this.Origin != null)
                {
                    val = this.Origin.Name;
                }
                if (!string.IsNullOrEmpty(this.CrossNum))
                {
                    val = val + " " + this.CrossNum;
                }
                if (!string.IsNullOrEmpty(this.ReciprocalString) && !this.ReciprocalString.ToLower().Equals("n"))
                {
                    val = val + this.ReciprocalString;
                }
                if (string.IsNullOrEmpty(val))
                {
                    val = "Unknown";
                }
                return val;
            }
        }

        public override string ToString()
        {
            return Name;
        }


    }

    public class FamilyMetaData
    {
        //[Display(Name = "Year")]
        //[RegularExpression("^[0-9]*$", ErrorMessage="The field Year can only contain numbers [0-9]")]
        //[StringLength(4, MinimumLength=4)] //length of a year
        //public string Year { get; set; }

        [Required]
        [Display(Name = "Genus")]
        public int GenusId { get; set; }

        [Required]
        [Display(Name = "Origin")]
        public int OriginId { get; set; }

        [Required]
        [Display(Name = "Cross #")]
        public string CrossNum { get; set; }

        //[Display(Name = "Note")]
        //[DataType(DataType.MultilineText)]
        //public string Note { get; set; }

        [Display(Name = "Cross Type")]
        public int CrossTypeId { get; set; }

        //[Display(Name = "Ploidy")]
        //public int PloidyId { get; set; }

        [Display(Name = "Field #")]
        public int? FieldPlantedNum { get; set; }

        [Display(Name = "Transplanted #")]
        public int? TransplantedNum { get; set; }

        [Display(Name = "Seed #")]
        public int? SeedNum { get; set; }

        [Display(Name = "Male")]
        public int MaleParent { get; set; }

        [Display(Name = "Female")]
        public int FemaleParent { get; set; }

        //[Display(Name = "Given Name")]
        //public string GivenName { get; set; }

        [Display(Name = "Is Reciprocal?")]
        public bool IsReciprocal { get; set; }

        [Display(Name = "Unsuccessful?")]
        public bool Unsuccessful { get; set; }


    }
}
