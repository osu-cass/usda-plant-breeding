using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Data.Models
{
    [MetadataType(typeof(NoteMetadata))]
    [DisplayColumn("Text")]
    [DisplayName("Note")]
    public partial class Note
    {
    }

    public class NoteMetadata
    {
        public int Id { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Required]
        public int GenotypeId { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
    }
}
