using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Data.Models
{
    [MetadataType(typeof(MapComponentMetadata))]
    public partial class MapComponent
    {
        public string GenotypeLabel
        {
            get
            {
                return "*Accession";
            }
        }


        public override string ToString()
        {
            return PickingNumber;
        }

       
    }

    public class MapComponentMetadata
    {


        [Display(Name = "Accession")]
        public int GenotypeId { get; set; }

        [Required]
        [Display(Name = "Row")]
        public int Row { get; set; }

        [Required]
        [Display(Name = "Map")]

        public int MapId { get; set; }
        public int Rep { get; set; }

        [Display(Name = "Plants Per Plot")]
        public int PlantsInRep { get; set; }

        [Required]
        [Display(Name = "Plot #")]
        public int PlantNum { get; set; }

        [Display(Name = "Picking #")]
        public string PickingNumber;


    }
}
