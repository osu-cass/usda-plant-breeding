using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usda.PlantBreeding.Data.Models;

namespace Usda.PlantBreeding.Core.Models
{
    public class TraitViewModel
    {
        public Genus Genus { get; set; }
        public IEnumerable<Question> RetiredQuestions { get; set; }
        public IEnumerable<Question> ActiveQuestions { get; set; }

    }
}
