using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Usda.PlantBreeding.Core.Models;
using Usda.PlantBreeding.Data.Models;

namespace Usda.PlantBreeding.Site.Models
{
    /// <summary>
    /// 
    /// choose a genotype
    /// take the male and the female from the genotype
    /// ploidy comes from genotype
    /// 
    /// find all cases where the genotype was ever planted
    /// 
    /// A container for information relevant to the Selection Summary.
    /// </summary>
    /// TODO: clean up seedling and observation
    [MetadataType(typeof(SelectionSummaryViewModelMetaData))]
    public class SelectionSummaryViewModel : IEqualityComparer<SelectionSummaryViewModel>
    {
        public string GenotypeNote { get; set; }
        public int GenotypeId { get; set; }
        public string GenotypeName { get; set; }
        public NotesViewModel Notes { get; set; }
        public FlatNotesViewModel FlatNotes { get; set; }
        public string MaleParentName { get; set; }
        public string FemaleParentName { get; set; }
        public string PloidyName { get; set; }

        public PhenotypeEntryViewModel PhenotypeSummaryVM { get; set; }

        public IEnumerable<FamilyUseViewModel> FamilyUseList { get; set; }
        public IEnumerable<ObservationData> ObservationList { get; set; }

        public bool Equals(SelectionSummaryViewModel x, SelectionSummaryViewModel y)
        {
            return this.GetHashCode(x) == this.GetHashCode(y);
        }

        public int GetHashCode(SelectionSummaryViewModel obj)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();

            builder.Append(obj.GenotypeId);
            builder.Append(obj.GenotypeName);
            builder.Append(obj.MaleParentName);
            builder.Append(obj.FemaleParentName);
            builder.Append(obj.PhenotypeSummaryVM.GetHashCode());

            foreach (var val in obj.FamilyUseList.OrderBy(f => f.GivenName)
                                                 .ThenBy(f => f.OriginName)
                                                 .ThenBy(f => f.PurposeName)
                                                 .ThenBy(f => f.Year)
                                                 .ThenBy(f => f.CrossWithName))
            {
                builder.Append(val.GetHashCode());
            }

            return builder.ToString().GetHashCode();
        }
    }

    public class SelectionSummaryViewModelMetaData
    {
        public int GenotypeId { get; set; }
        [Display(Name = "Selection Summary Notes")]
        public string GenotypeNote { get; set; }
        [Display(Name = "Accession")]
        public string GenotypeName { get; set; }
        [Display(Name = "Male Parent")]
        public string MaleParentName { get; set; }
        [Display(Name = "Female Parent")]
        public string FemaleParentName { get; set; }
        [Display(Name = "Ploidy")]
        public string PloidyName { get; set; }
    }

    public class ObservationData
    {
        public DateTime ObservationDate { get; set; }
        public string ObservationNote { get; set; }
    }
}