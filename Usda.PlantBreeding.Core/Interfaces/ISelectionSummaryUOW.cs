using System.Collections.Generic;
using System.Web.Mvc;
using Usda.PlantBreeding.Core.Models;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Site.Models;

namespace Usda.PlantBreeding.Core.Interfaces
{
    public interface ISelectionSummaryUOW
    {
       SelectionSummaryViewModel GetSelectionSummary(int id);
       IEnumerable<GenotypeUseSummaryViewModel> GetCrossSelections(int id);

       void UpdateGenotypePloidy(int genotypeId, string ploidy);
       void UpdateGenotypeNote(int genotypeId, string note);
    }
}
