using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Data.Models
{
    [MetadataType(typeof(CrossPlanMetadata))]
    public partial class CrossPlan
    {

        
    }

    public class CrossPlanMetadata
    {
        [RegularExpression("^[0-9]*$", ErrorMessage = "The field Year can only contain numbers [0-9]")]
        [StringLength(4, MinimumLength = 4)] //length of a year
        public string Year { get; set; }

        public Nullable<int> MaleParentId { get; set; }
        public Nullable<int> FemaleParentId { get; set; }

        public string Purpose { get; set; }
        public bool Reciprocal { get; set; }
        public string Note { get; set; }
        public string CrossNum { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<int> CrossTypeId { get; set; }
        public Nullable<int> OriginId { get; set; }
        public Nullable<int> SeedNum { get; set; }
        public Nullable<int> FieldPlantedNum { get; set; }
        public Nullable<int> TransplantedNum { get; set; }
    }
}
