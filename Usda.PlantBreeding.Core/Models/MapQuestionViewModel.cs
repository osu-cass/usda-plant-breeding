using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Usda.PlantBreeding.Core.Models
{
    [MetadataType(typeof(MapQuestionViewModelMetadata))]
    public class MapQuestionViewModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int GenusId { get; set; }

        public bool Required { get; set; }

        public bool? Retired { get; set; }

        public string Label { get; set; }

        public bool isChecked { get; set; }

        public short Order { get; set; }


    }
    public class MapQuestionViewModelComparer : IEqualityComparer<MapQuestionViewModel>
    {
        public bool Equals(MapQuestionViewModel x, MapQuestionViewModel y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null && y != null)
            {
                return false;
            }
            else if (x != null && y == null)
            {
                return false;
            }
            else
            {
                return x.Id == y.Id;
            }
        }

        public int GetHashCode(MapQuestionViewModel obj)
        {
            return obj.Id.GetHashCode();
        }
    }


    public class MapQuestionViewModelMetadata
    {
        public int Id { get; set; }

        [Display(Name = "Question")]
        public string Text { get; set; }

        public int GenusId { get; set; }

        public bool Required { get; set; }

        public Nullable<bool> Retired { get; set; }

        [Display(Name = "Help Text")]
        public string Label { get; set; }
        [Display(Name = "Add to Map")]
        public bool isChecked { get; set; }

    }

}