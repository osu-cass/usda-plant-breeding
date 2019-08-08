using System.Collections.Generic;
using System.Web.Mvc;
using Usda.PlantBreeding.Core.Models;
using Usda.PlantBreeding.Data.Models;

namespace Usda.PlantBreeding.Core.Interfaces
{
    public interface IPhenotypeEntryUOW
    {
        IEnumerable<DataListViewModel> SearchAccessions(string term, int genusId, int? mapId, bool mapOnly, int recordsToReturn);
        MapComponentSummaryVM CreateSeedlingSelection(SeedlingViewModel seedlingViewModel);
        PhenotypeEntryViewModel GetPhenotypeSummary(int genotypeId);
        PhenotypeEntryViewModel GetPhenotypeEntryVM(int id, string year);
        PhenotypeRowsViewModel GetPhenotypeMapRows(int id, string year);
        PhenotypeRowsViewModel GetGenotypeRows(int id);

        void SaveAnswer(AnswerViewModel answervm);
        void SaveRowNum(MapComponentSummaryVM summary);
        void SavePlantNum(MapComponentSummaryVM summary);
        void SaveComments(MapComponentSummaryVM summary);
        void SaveCreatedDate(MapComponentSummaryVM summary);
    }
}
