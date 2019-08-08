using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Data.Models
{
    public partial class Answer : IEqualityComparer<Answer>
    {
        public bool Equals(Answer x, Answer y)
        {
            return x.QuestionId.Equals(y.QuestionId);
        }

        public int GetHashCode(Answer obj)
        {
            return obj.QuestionId.GetHashCode();
        }
    }
}
