using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usda.PlantBreeding.Core.Interfaces;
using Usda.PlantBreeding.Core.Infrastructure;

namespace Usda.PlantBreeding.Core.Models
{
    public class MapComponentViewModel : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Row# is requried")]
        public int? Row { get; set; }

        [Required(ErrorMessage = "MapId is required")]
        public int MapId { get; set; }
        public int Rep { get; set; }
        public int PlantsInRep { get; set; }
        public int? GenotypeId { get; set; }
        public string GenotypeName { get; set; }
        [Required(ErrorMessage = "Plant# is requried")]
        [Display(Name="Plot #")]
        public int? PlantNum { get; set; }
        public bool? isSeedling { get; set; }
        public int? ExternalId { get; set; }
        public string PickingSuffix { get; set; }
        public string PickingNumber { get; set; }
        public string Virus4 { get; set; }
        public string Virus1 { get; set; }
        public string Virus2 { get; set; }
        public string Virus3 { get; set; }
        public int YearsId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            IMapUOW repo = new MapUow();

            if (PlantNum.HasValue && Row.HasValue && repo.IsDuplicateRow(Id, MapId, Row.Value, PlantNum.Value, YearsId))
            {
                errors.Add(new ValidationResult("Duplicate Row", new[] { "PlantNum", "Row" }));

            }
            return errors;

        }


    }
}

