using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Usda.PlantBreeding.Data.Models;


namespace Usda.PlantBreeding.Core.Models
{
    public class CrossPlanViewModel
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "Required field", AllowEmptyStrings = true)]
        [RegularExpression(@"^[1-9][0-9]{3}$", ErrorMessage = "Invalid year YYYY")]
        [StringLength(4, ErrorMessage = "Invalid year YYYY")] //length of a year
        [Display(Name = "Year")]
        public string Year { get; set; }
        public int? GenotypeId { get; set; }


        [Display(Name = "Father")]
        public int? MaleParentId { get; set; }

        [Display(Name = "Father")]
        public string MaleParentName { get; set; }


        [Display(Name = "Mother")]
        public int? FemaleParentId { get; set; }

        [Display(Name = "Mother")]
        public string FemaleParentName { get; set; }

        public string FemaleParentOriginName { get; set; }
        public string FemaleParentCrossNum { get; set; }
        public string FemaleParentReciprocalString { get; set; }

        public int? FemaleSelectionNumber { get; set; }

        [Display(Name = "Purpose")]
        public string Purpose { get; set; }

        [Display(Name = "Not Succ?")]
        public bool Unsuccessful { get; set; }

        [Display(Name = "Created")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime DateCreated { get; set; }

        [Display(Name = "F.Father")]
        public string MaleParentMaleParentOrDefault
        {
            get
            {
                return string.IsNullOrWhiteSpace(MaleParentMaleParent) ? "-" : MaleParentMaleParent;
            }
        }

        [Display(Name = "F.Father")]
        public string MaleParentMaleParent { get; set; }

        [Display(Name = "F.Mother")]
        public string MaleParentFemaleParentOrDefault
        {
            get
            {
                return string.IsNullOrWhiteSpace(MaleParentFemaleParent) ? "-" : MaleParentFemaleParent;
            }
        }

        [Display(Name = "F.Mother")]
        public string MaleParentFemaleParent { get; set; }

        [Display(Name = "M.Father")]
        public string FemaleParentMaleParentOrDefault
        {
            get
            {
                return string.IsNullOrWhiteSpace(FemaleParentMaleParent) ? "-" : FemaleParentMaleParent;
            }
        }

        [Display(Name = "M.Father")]
        public string FemaleParentMaleParent { get; set; }

        [Display(Name = "M.Mother")]
        public string FemaleParentFemaleParentOrDefault
        {
            get
            {
                return string.IsNullOrWhiteSpace(FemaleParentFemaleParent) ? "-" : FemaleParentFemaleParent;
            }
        }

        [Display(Name = "M.Mother")]
        public string FemaleParentFemaleParent { get; set; }

        [Display(Name = "Genus")]
        public int GenusId { get; set; }

        [Display(Name = "Genus")]
        public string GenusName { get; set; }

        [Display(Name = "Recip")]
        public bool Reciprocal { get; set; }

        [Display(Name = "Cross Note")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

        [Display(Name = "Cross Num")]
        public string CrossNum { get; set; }

        [Display(Name = "Cross Type")]
        public int? CrossTypeId { get; set; }
        public string CrossTypeName { get; set; }

        public int? OriginId { get; set; }
        public string OriginName { get; set; }

        [Display(Name = "Seed Num")]
        public int? SeedNum { get; set; }

        [Display(Name = "Field Planted")]
        public int? FieldPlantedNum { get; set; }

        [Display(Name = "Trans Num")]
        public int? TransplantedNum { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", this.FemaleParentName, this.MaleParentName);
        }

        public int newIndex { get; set; }
        public int Index
        {
            get
            {
                return (Id <= 0) ? newIndex : Id;
            }
        }
    }

}