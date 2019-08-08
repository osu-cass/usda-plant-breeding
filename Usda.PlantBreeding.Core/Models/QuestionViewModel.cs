using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Core.Models
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Required { get; set; }
        public string Label { get; set; }
        public int Order { get; set; }
        public string InputType { get; set; }
    }
}
