using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Data.Models
{
    [MetadataType(typeof(QuestionMetadata))]
    [DisplayColumn("Text")]
    public partial class Question
    {
        public override string ToString()
        {
            return Label;
        }
    }

    public class QuestionMetadata
    {
        public int Id { get; set; }

        [Display(Name = "Question")]
        public string Text { get; set; }

        public int GenusId { get; set; }
        [Display(Name = "Default")]

        public bool Required { get; set; }
        public Nullable<bool> Retired { get; set; }

        [Display(Name = "Label")]
        public string Label { get; set; }


    }
}
