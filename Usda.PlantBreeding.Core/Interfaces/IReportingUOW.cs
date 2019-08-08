using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usda.PlantBreeding.Core.Models;
using Usda.PlantBreeding.Core.Models.Reporting;

namespace Usda.PlantBreeding.Core.Interfaces
{
    public interface IReportingUOW
    {
        MapViewReportViewModel GetMapViewReportViewModel(int? mapId, string year = null);

        MapComponentReportViewModel GetMapComponentReportViewModel(int? mapId, string year = null);

        PhenotypeReportViewModel GetPhenotypeReportViewModel(int? mapId, string year = null);

        CrossPlanReportViewModel GetCrossPlanReportViewModel(int? genusId, string year);
        CrossPlanLocationReportViewModel GetCrossPlanLocationReportViewModel(int? genusId, string year);

        AccessionReportViewModel GetAccessionReportViewModel(int? genusId, string year);
        AccessionReportViewModel GetAccessionsCrossesMadeReportViewModel(int? genusId, string year);

        MapInventoryReportViewModel GetMapInventoryReportViewModel(int? mapId, string year = null);
        MapComponentFatesReportViewModel GetMapComponentFatesReportViewModel(int? genusId);
        BreedingSummaryReportViewModel GetBreedingSummaryReportViewModel(int? genusId);
        DistributionReportViewModel GetDistributionReportViewModel(string year, int? locationId, int? genotypeId);


        ReportViewModel GetReportForMapInventory(MapInventoryReportViewModel vm);
        ReportViewModel GetReportForMapComponent(MapComponentReportViewModel vm);

        ReportViewModel GetReportForAccession(AccessionReportViewModel vm);
        ReportViewModel GetReportForAccessionParents(AccessionReportViewModel vm);

        ReportViewModel GetReportForCrossPlanLocation(CrossPlanLocationReportViewModel vm);
        ReportViewModel GetReportForCrossPlans(CrossPlanReportViewModel vm);

        ReportViewModel GetReportForMapView(MapViewReportViewModel vm);
        ReportViewModel GetReportForPhenotype(PhenotypeReportViewModel vm);

        ReportViewModel GetReportForMapComponentFates(MapComponentFatesReportViewModel vm);
        ReportViewModel GetReportForBreedingSummaryReportViewModel(BreedingSummaryReportViewModel vm);
        ReportViewModel GetSelectionSummaryReport(SelectionSummaryReport vm);

        ReportViewModel GetReportForDistributions(DistributionReportViewModel vm);


    }
}
