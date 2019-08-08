using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Usda.PlantBreeding.Core.Infrastructure;
using Usda.PlantBreeding.Core.Interfaces;
using Usda.PlantBreeding.Data.Models;

namespace Usda.PlantBreeding.Core.Models
{
    public class AccessionViewModel : IValidatableObject
    {
        public int FamilyId { get; set; }
        public int? CrossPlanId { get; set; }

        [Display(Name = "Genus")]
        public int FamilyGenusId { get; set; }

        [Display(Name = "Origin")]
        public int? FamilyOriginId { get; set; }

        public string FamilyOriginName { get; set; }

        [Display(Name = "Cross Type")]
        public int? FamilyCrossTypeId { get; set; }

        [Display(Name = "Cross #")]
        public string FamilyCrossNum { get; set; }

        [Display(Name = "Field #")]
        public int? FamilyFieldPlantedNum { get; set; }

        [Display(Name = "Transplanted #")]
        public int? FamilyTransplantedNum { get; set; }

        [Display(Name = "Seed #")]
        public int? FamilySeedNum { get; set; }

        [Display(Name = "Male")]
        public int? FamilyMaleParent { get; set; }

        public string FamilyMaleGenotypeName { get; set; }

        [Display(Name = "Female")]
        public int? FamilyFemaleParent { get; set; }

        public string FamilyFemaleGenotypeName { get; set; }

        [Display(Name = "Is Reciprocal?")]
        public bool FamilyIsReciprocal { get; set; }

        [Display(Name = "Purpose")]
        public string FamilyPurpose { get; set; }

        [Display(Name = "Unsuccessful?")]
        public bool FamilyUnsuccessful { get; set; }

        public int Id { get; set; }


        [Display(Name = "Ploidy")]

        public string PloidyName { get; set; }

        [Display(Name = "Cross Year")]
        public string FamilyBaseGenotypeYear { get; set; }

        public string Year { get; set; }

        [Display(Name = "Population")]
        public bool IsPopulation { get; set; }

        [Display(Name = "Cross Note")]
        [DataType(DataType.MultilineText)]
        public string FamilyBaseGenotypeNote { get; set; }

        [Display(Name = "Patent Application")]
        public string PatentApp { get; set; }

        [Display(Name = "Patent#")]
        public string Patent { get; set; }

        [Display(Name = "Patent Year")]
        public string PatentYear { get; set; }

        [Display(Name = "Selection Summary Notes")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

        [Display(Name = "Name")]
        public string GivenName { get; set; }

        [Display(Name = "Original Name")]
        public string OriginalName { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Fate")]
        public string Fate { get; set; }

        [Display(Name = "Selection #")]
        public int? SelectionNum { get; set; }

        [Display(Name = "Is Root?")]
        public bool IsRoot { get; set; }

        [Display(Name = "Is Root?")]
        public bool IsBase { get; set; }

        [Display(Name = "Alias 1")]
        public string Alias1 { get; set; }

        [Display(Name = "Alias 2")]
        public string Alias2 { get; set; }

        [Display(Name = "Field Status")]
        public IEnumerable<MapComponent> MapComponents { get; set; }

        [Display(Name = "Retired Field Status")]
        public IEnumerable<MapComponent> MapComponentsRetired { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // TODO: make private field of view model
            IAccessionUOW a_repo = new AccessionUOW();
            // error messages
            const string populationError = "Duplicate origin and cross number combination.";
            const string selectionError = "Duplicate selection number. Next selection number is ";
            const string yearError = "Please enter a valid year YYYY";
            const string requiredError = "Required field";
            // list of errors
            List<ValidationResult> errors = new List<ValidationResult>();
            // validate population
            if (this.IsBase)
            {
                if (this.FamilyGenusId <= 0)
                {
                    errors.Add(new ValidationResult(requiredError, new[] { "FamilyGenusId" }));
                }
                if (!this.FamilyOriginId.HasValue || this.FamilyOriginId.Value <= 0)
                {
                    errors.Add(new ValidationResult(requiredError, new[] { "FamilyOriginId" }));
                }
                if (this.FamilyCrossNum == null)
                {
                    errors.Add(new ValidationResult(requiredError, new[] { "FamilyCrossNum" }));
                }
                if (this.FamilyOriginId.HasValue && a_repo.IsDuplicatePopulation(this.FamilyId, this.FamilyOriginId.Value, this.FamilyCrossNum, this.FamilyGenusId))
                {
                    errors.Add(new ValidationResult(populationError, new[] { "FamilyOriginId", "FamilyCrossNum" }));
                }
            }

            // validate year
            Regex yearPattern = new Regex(@"^[1-2][0-9]{3}$");
            if (this.IsBase)
            {
                if (!string.IsNullOrWhiteSpace(this.FamilyBaseGenotypeYear) && !yearPattern.IsMatch(this.FamilyBaseGenotypeYear))
                {
                    errors.Add(new ValidationResult(yearError, new[] { "FamilyBaseGenotypeYear" }));
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(this.Year) && !yearPattern.IsMatch(this.Year))
                {
                    errors.Add(new ValidationResult(yearError, new[] { "Year" }));
                }
            }

           
            // validate selection number
            if (this.SelectionNum.HasValue && a_repo.IsDuplicateSelection(this.Id, this.SelectionNum.Value, this.FamilyId))
            {
                errors.Add(new ValidationResult(selectionError + a_repo.GetNextSelection(this.FamilyId), new[] { "SelectionNum" }));
            }
            return errors;
        }
    }
}