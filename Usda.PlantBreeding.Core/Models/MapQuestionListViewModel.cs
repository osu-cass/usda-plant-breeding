using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Usda.PlantBreeding.Data.Models;

namespace Usda.PlantBreeding.Core.Models
{
    /// <summary>
    /// Contains a new map and set of questions that aressociated with a genus id
    /// </summary>
    public class MapQuestionListViewModel : IEqualityComparer<MapQuestionListViewModel>
    {
        /// <summary>
        /// Map holds the genusid, its other properties are loaded as part of the create view
        /// </summary>
        public Map Map { get; set; }

        public int MapId { get; set; }

        /// <summary>
        /// Questions that are associated with a given genus id, part of global state
        /// </summary>
        public List<MapQuestionViewModel> Questions { get; set; }

        public bool Equals(MapQuestionListViewModel x, MapQuestionListViewModel y)
        {
            return this.GetHashCode(x) == this.GetHashCode(y);
        }

        public int GetHashCode(MapQuestionListViewModel obj)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();

            builder.Append(obj.Map.GenusId);

            foreach (MapQuestionViewModel q in obj.Questions.OrderBy(q => q.Id))
            {
                builder.Append(q.Id);
                builder.Append(q.Text);
            }

            return builder.ToString().GetHashCode();
        }
        public override string ToString()
        {
            string val = "";
            if (Map != null)
            {
                val = Map.ToString();
            }
            return val;
        }
    }
}