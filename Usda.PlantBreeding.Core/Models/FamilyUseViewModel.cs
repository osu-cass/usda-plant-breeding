using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Usda.PlantBreeding.Data.Models;

namespace Usda.PlantBreeding.Core.Models
{
    [MetadataType(typeof(FamilyUseViewModelMetaData))]
    public class FamilyUseViewModel : IEqualityComparer<FamilyUseViewModel>
    {
        public string OriginName { get; set; }
        public string GivenName { get; set; }
        [Display (Name = "Seed")]
        public int? SeedNumber { get; set; }
        [Display(Name = "Field")]
        public int? FieldNumber { get; set; }
        public string PurposeName { get; set; }
        public int Id { get; set; }
        public string Year { get; set; }
        public string CrossWithName { get; set; }
        public int SelectionCount { get; set; }

        /// <summary>
        /// This will be the original name or given name
        /// </summary>
        public string Name { get; set; }
        public bool Equals(FamilyUseViewModel x, FamilyUseViewModel y)
        {
            return this.GetHashCode(x) == this.GetHashCode(y);
        }

        public int GetHashCode(FamilyUseViewModel obj)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();

            builder.Append(obj.OriginName);
            builder.Append(obj.GivenName);
            builder.Append(obj.PurposeName);
            builder.Append(obj.Year);
            builder.Append(obj.CrossWithName);

            return builder.ToString().GetHashCode();
        }
    }

    public class FamilyUseViewModelMetaData
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Family Cross Given")]
        public string GivenName { get; set; }

        [Display(Name = "Purpose")]
        public string PurposeName { get; set; }

        [Display(Name = "Year")]
        public string Year { get; set; }

        [Display(Name = "Parent")]
        public string CrossWithName { get; set; }
        [Display(Name = "Selections")]

        public int SelectionCount { get; set; }

    }
}