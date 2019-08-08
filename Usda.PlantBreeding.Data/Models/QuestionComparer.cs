using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Data.Models
{
    /// <summary>
    /// Provides the means to compare questions by their ID value.
    /// </summary>
    public class QuestionComparer : IEqualityComparer<Question>
    {
        public bool Equals(Question x, Question y)
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

        public int GetHashCode(Question obj)
        {
            return obj.Id.GetHashCode();
        }
    }
    
}
