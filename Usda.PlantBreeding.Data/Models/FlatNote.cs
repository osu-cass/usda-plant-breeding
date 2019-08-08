using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Data.Models
{
    [MetadataType(typeof(FlatNoteMetaData))]
    [DisplayColumn("Text")]
    [DisplayName("Flat Note")]
    public partial class FlatNote
    {
    }

    public class FlatNoteMetaData
    {
        public string Other { get; set; }
        [Display(Name = "Type")]
        public int FlatTypeId { get; set; }

        public int Id { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Required]
        public int GenotypeId { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
