using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using BSGUtils;
using System;
namespace Usda.PlantBreeding.Data.Models
{
    [MetadataType(typeof(GenotypeMetaData))]
    [DisplayColumn("Name")]
    [DisplayName("Accession")]
    public partial class Genotype 
    {
        public string Name
        {
            get
            {
                string val = (!string.IsNullOrEmpty(this.GivenName) ? this.GivenName : this.OriginalName);
                return (string.IsNullOrEmpty(val)) ? "" : val;
            }
        }
        public bool IsBase
        {
            get
            {
                return (this.Id == this.Family.BaseGenotypeId);
            }
        }

        // TODO: rename this with capitol letter "O"
      
        public override string ToString()
        {
            return this.Name;
        }

        
    }
    public class GenotypeMetaData
    {
        //[RegularExpression("^[0-9]*$", ErrorMessage = "The field Year can only contain numbers [0-9]")]
        //[StringLength(4, MinimumLength = 4)] // length of a year
        public bool IsPopulation { get; set; }
        public string Year { get; set; }

        [Display(Name = "Note")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

        [Display(Name = "Fate")]
        public string Fate { get; set; }

        [Display(Name = "Selection #")]
        public Nullable<int> SelectionNum { get; set; }

        [Display(Name = "Given Name")]
        public string GivenName { get; set; }

        [Display(Name = "Family")]
        public int FamilyId { get; set; }

        [Display(Name = "External Id")]
        public Nullable<int> ExternalId { get; set; }

        [Display(Name = "Original Name")]
        public string OriginalName { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        public string Alias1 { get; set; }
        public string Alias2 { get; set; }

        [Display(Name="Patent Application")]
        public string PatentApp { get; set; }

        [Display(Name="Patent#")]
        public string Patent { get;set; }

        [Display(Name="Patent Year")]
        public string PatentYear { get; set; }
    }
}
