using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Core.Models
{
    public class AccessionIndexViewModel
    {
        public int Id { get; set; }
        public int FamilyId { get; set; }
        [Display(Name = "Name")]

        public string Name
        {
            get
            {
                string val = (string.IsNullOrWhiteSpace(GivenName)) ? OriginalName : GivenName;
                val = (string.IsNullOrWhiteSpace(val)) ? string.Empty : val;
                return val;
            }
        }
        [Display(Name = "Original Name")]
        public string OriginalName { get; set; }

        public string FamilyReciprocalString { get; set; }

        [Display(Name = "Name")]
        public string GivenName { get; set; }

        public int? SelectionNum { get; set; }

        public string FamilyOriginName { get; set; }
        public string FamilyCrossNum { get; set; }
        [Display(Name = "Purpose")]

        public string FamilyPurpose { get; set; }

        #region familyFemale
        [Display(Name = "Mom")]
        public int? FamilyFemaleParent { get; set; }
        [Display(Name = "Mom")]

        public string FamilyFemaleGenotypeName
        {
            get
            {
                string val = (string.IsNullOrWhiteSpace(FamilyFemaleGenotypeGivenName)) ? FamilyFemaleParentOriginalName : FamilyFemaleGenotypeGivenName;
                val = (string.IsNullOrWhiteSpace(val)) ? string.Empty : val;
                return val;
            }
        }
        public string FamilyFemaleParentOriginalName { get; set; }
        public string FamilyFemaleGenotypeGivenName { get; set; }
        #endregion

        #region familyMale
        [Display(Name = "Dad")]
        public int? FamilyMaleParent { get; set; }

        [Display(Name = "Dad")]
        public string FamilyMaleGenotypeName
        {
            get
            {
                
                string val = (string.IsNullOrWhiteSpace(FamilyMaleGenotypeGivenName)) ? FamilyMaleParentOriginalName : FamilyMaleGenotypeGivenName;
                val = (string.IsNullOrWhiteSpace(val)) ? string.Empty : val;
                return val;
            }
        }
        public string FamilyMaleParentOriginalName {get;set;}
      
    

        public string FamilyMaleGenotypeGivenName { get; set; }
        #endregion
        [Display(Name = "Year")]
        public string Year { get; set; }

        [Display(Name = "Popul")]
        public bool IsPopulation { get; set; }

        [Display(Name = "Ploidy")]
        public string PloidyName { get; set; }

        [Display(Name = "Type")]
        public string FamilyCrossTypeName { get; set; }

        [Display(Name = "Notes")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }



    }
}
