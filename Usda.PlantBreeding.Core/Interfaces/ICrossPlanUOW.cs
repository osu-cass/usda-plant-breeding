using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Usda.PlantBreeding.Core.Models;
using Usda.PlantBreeding.Data.Models;

namespace Usda.PlantBreeding.Core.Interfaces
{
    public interface ICrossPlanUOW
    {
        IEnumerable<DataListViewModel> SearchAccessions(string term, int genusId, int recordsToReturn);
        AccessionViewModel GetNextAccession(int crossId);
        void Save(CrossPlanViewModel crossPlan);
        void SaveFamily(int crossPlanId);

        void SaveIndex(CrossPlanIndexViewModel crossIndex);
        void Delete(CrossPlanViewModel crossPlan);
        CrossPlanIndexViewModel GetIndex(string year, int genusId);
        IEnumerable<CrossPlanViewModel> GetCrossPlans(string year, int genusId);
        CrossPlanViewModel GetNextCrossPlan(string year, int genusId);
        CrossPlanViewModel GetCrossPlan(int id);
        IEnumerable<SelectListItem> GetYears(string selectedYear, int genusId);

        ParentViewModel GetParents(int genotypeId);
        IEnumerable<SelectListItem> GetCrossTypesList(int genusId);

        void SaveAccession(AccessionViewModel accession);
        void CopyToNewYear(CrossPlanViewModel cpvm);
        IEnumerable<CrossPlanViewModel> DefaultOrder(IEnumerable<CrossPlanViewModel> crossPlans);

        IEnumerable<CrossPlanViewModel> OrderByParents(IEnumerable<CrossPlanViewModel> crossPlans);
        void SaveAccession(CrossPlanViewModel cpvm);
        CrossPlanViewModel CreateNextCrossPlan(string year, int genusId);
        IEnumerable<CrossPlanViewModel> GenerateReciprocals(string year, int genusId);



    }
}
