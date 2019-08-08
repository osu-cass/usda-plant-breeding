using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSGUtils;
using System.ComponentModel.DataAnnotations.Schema;
namespace Usda.PlantBreeding.Data.Models
{
    [MetadataType(typeof(MapMetadata))]
    [DisplayColumn("Name")]
    public partial class Map
    {


        public string NameLabel
        {
            get
            {
                return "*Name";
            }
        }

        public string GenusLabel
        {
            get
            {
                return "*Genus";
            }
        }
        public string EvaluationYear { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", this.Name, this.PlantingYear);
        }

        public string FullName
        {
            get
            {
                return this.ToString();
            }
        }

    }

    public class MapMetadata
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Established Year")]
        public string PlantingYear { get; set; }

        [Required]
        [Display(Name = "Genus")]
        public int GenusId { get; set; }

        [Required]
        [Display(Name = "Location")]
        public int LocationId { get; set; }

        [Display(Name = "Seedling Map")]
        public bool IsSeedlingMap { get; set; }

        [Display(Name = "Picking Prefix")]
        public string PicklistPrefix { get; set; }

        [Required]
        [Display(Name = "Default # Plants")]
        public int DefaultPlantsInRep { get; set; }

        [Display(Name = "Eval Year")]
        public string EvaluationYear { get; set; }

        [Required]
        [Display(Name = "Cross Type")]
        public int? CrossTypeId { get; set; }

        [Display(Name = "Description Code")]
        public string LocationSuffix { get; set; }
        [Display(Name= "Evaluation Year")]
       public ICollection<Years> Years { get; set; }



    }
}
