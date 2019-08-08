using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using Usda.PlantBreeding.Core.Models;
using Usda.PlantBreeding.Core.Models.Reporting;

namespace Usda.PlantBreeding.Core.Translations
{
    public static class ReportViewModelTranslator
    {
        private static ReportViewModel GetReportViewModel(List<ReportParameter> reportparams, string reportName, string reportTitle)
        {
            return new ReportViewModel()
            {
                parameters = reportparams,
                ReportName = reportName,
                ReportTitle = reportTitle
            };

        }
        public static ReportViewModel ToReportViewModel(this MapViewReportViewModel vm, string reportName, string reportTitle)
        {
            //TODO ?: Create report attribute for Report View models and use reflection to build the report params dynamically
            string paramMapId = "MapId";
            string paramMapYear = "Year";
            string paramMapName = "MapName";
            string paramPrintPick = "ShowPicking";
            string paramMapComponentSeedling = "MapComponentSeedlingFlag";
            string paramReportTitle = "ReportTitle";
            bool? seedlingMC = null;

            if (vm.SeedlingComponentValue == "Planted")
            {
                seedlingMC = true;
            }
            else if (vm.SeedlingComponentValue == "SelectionsMade")
            {
                seedlingMC = false;
            }

            List<ReportParameter> reportparameters = new List<ReportParameter>();
            reportparameters.Add(new ReportParameter(paramMapId, vm.MapId.ToString()));
            reportparameters.Add(new ReportParameter(paramMapYear, vm.MapYear));
            reportparameters.Add(new ReportParameter(paramMapName, vm.MapName));
            reportparameters.Add(new ReportParameter(paramPrintPick, vm.PrintPicking.ToString()));
            reportparameters.Add(new ReportParameter(paramMapComponentSeedling, seedlingMC.HasValue ? seedlingMC.Value.ToString() : null));
            reportparameters.Add(new ReportParameter(paramReportTitle, reportTitle));
            return GetReportViewModel(reportparameters, reportName, reportTitle);
          
        }

        public static ReportViewModel ToReportViewModel(this CrossPlanReportViewModel vm, string reportName, string reportTitle)
        {
            //TODO ?: Create report attribute for Report View models and use reflection to build the report params dynamically
            string paramGenusId = "GenusId";
            string paramYear = "Year";
            string paramName = "Name";
            List<ReportParameter> reportparameters = new List<ReportParameter>();
            reportparameters.Add(new ReportParameter(paramGenusId, vm.GenusId.ToString()));
            reportparameters.Add(new ReportParameter(paramYear, vm.Year));
            reportparameters.Add(new ReportParameter(paramName, reportTitle));
            return GetReportViewModel(reportparameters, reportName, reportTitle);

        }

        public static ReportViewModel ToReportViewModel(this CrossPlanLocationReportViewModel vm, string reportName, string reportTitle) 
        {
            //TODO ?: Create report attribute for Report View models and use reflection to build the report params dynamically
            string paramGenusId = "GenusId";
            string paramYear = "Year";
            string paramReportTitle = "ReportTitle";
            string virus1Label = "VirusLabel1";
            string virus2Label = "VirusLabel2";
            string virus3Label = "VirusLabel3";
            string virus4Label = "VirusLabel4";


            List<ReportParameter> reportparameters = new List<ReportParameter>();
            reportparameters.Add(new ReportParameter(paramGenusId, vm.GenusId.ToString()));
            reportparameters.Add(new ReportParameter(paramYear, vm.Year));
            reportparameters.Add(new ReportParameter(paramReportTitle, reportTitle));
            reportparameters.Add(new ReportParameter(virus1Label, vm.Virus1Label.GetValueOrBlank()));
            reportparameters.Add(new ReportParameter(virus2Label, vm.Virus2Label.GetValueOrBlank()));
            reportparameters.Add(new ReportParameter(virus3Label, vm.Virus3Label.GetValueOrBlank()));
            reportparameters.Add(new ReportParameter(virus4Label, vm.Virus4Label.GetValueOrBlank()));

            return GetReportViewModel(reportparameters, reportName, reportTitle);

        }
        public static ReportViewModel ToReportViewModel(this DistributionReportViewModel vm, string reportName, string reportTitle)
        {
            //TODO ?: Create report attribute for Report View models and use reflection to build the report params dynamically
            string paramYear = "Year";
            string paramLocationId = "LocationId";
            string paramGenotypeId = "GenotypeId";
            
            List<ReportParameter> reportparameters = new List<ReportParameter>();
            reportparameters.Add(new ReportParameter(paramYear, (vm.Year != null && vm.Year.Length > 0) ? vm.Year : null));
            reportparameters.Add(new ReportParameter(paramLocationId, vm.LocationId.HasValue ? vm.LocationId.Value.ToString() : null));
            reportparameters.Add(new ReportParameter(paramGenotypeId, vm.GenotypeId.HasValue ? vm.GenotypeId.Value.ToString() : null));

            return GetReportViewModel(reportparameters, reportName, reportTitle);

        }


        public static ReportViewModel ToReportViewModel(this MapComponentReportViewModel vm, string reportName, string reportTitle)
        {
            //TODO ?: Create report attribute for Report View models and use reflection to build the report params dynamically
            string paramMapId = "MapId";
            string paramMapYear = "Year";
            string paramMapName = "MapName";
            string virus1Label = "Virus1Label";
            string virus2Label = "Virus2Label";
            string virus3Label = "Virus3Label";
            string virus4Label = "Virus4Label";
            string paramReportTitle = "ReportTitle";

            string paramMapComponentSeedling = "MapComponentSeedlingFlag";
            bool? seedlingMC = null;

            if (vm.SeedlingComponentValue == "Planted")
            {
                seedlingMC = true;
            }
            else if (vm.SeedlingComponentValue == "SelectionsMade")
            {
                seedlingMC = false;
            }

            List<ReportParameter> reportparameters = new List<ReportParameter>();
            reportparameters.Add(new ReportParameter(paramMapId, vm.MapId.ToString()));
            reportparameters.Add(new ReportParameter(paramMapYear, vm.MapYear));
            reportparameters.Add(new ReportParameter(paramMapName, vm.MapName));
            reportparameters.Add(new ReportParameter(virus1Label, vm.Virus1Label.GetValueOrBlank()));
            reportparameters.Add(new ReportParameter(virus2Label, vm.Virus2Label.GetValueOrBlank()));
            reportparameters.Add(new ReportParameter(virus3Label, vm.Virus3Label.GetValueOrBlank()));
            reportparameters.Add(new ReportParameter(virus4Label, vm.Virus4Label.GetValueOrBlank()));
            reportparameters.Add(new ReportParameter(paramMapComponentSeedling, seedlingMC.HasValue ? seedlingMC.Value.ToString() : null));
            reportparameters.Add(new ReportParameter(paramReportTitle, reportTitle));

            return GetReportViewModel(reportparameters, reportName, reportTitle);

        }

        public static ReportViewModel ToReportViewModel(this PhenotypeReportViewModel vm, string reportName, string reportTitle)
        {
            //TODO ?: Create report attribute for Report View models and use reflection to build the report params dynamically
            string paramMapId = "MapId";
            string paramMapYear = "Year";
            string paramReportTitle = "ReportTitle";

            List<ReportParameter> reportparameters = new List<ReportParameter>();
            reportparameters.Add(new ReportParameter(paramMapId, vm.MapId.ToString()));
            reportparameters.Add(new ReportParameter(paramMapYear, vm.MapYear));
            reportparameters.Add(new ReportParameter(paramReportTitle, reportTitle));

            return GetReportViewModel(reportparameters, reportName, reportTitle);

        }

        public static ReportViewModel ToReportViewModel(this AccessionReportViewModel vm, string reportName, string reportTitle)
        {
            string paramGenusId = "GenusId";
            string paramYear = "Year";
            string paramInternalFlag = "InternalFlag";
            string paramCrossesFlag = "CrossesFlag";
            string paramReportTitle = "ReportTitle";

            bool? internalFlag = null;
            bool? crossesFlag = null;

            if (vm.OriginOptionValue == "Internal")
            {
                internalFlag = true;
            }
            else if (vm.OriginOptionValue == "External")
            {
                internalFlag = false;
            }

            if(vm.AccessionOptionValue == "Populations")
            {
                crossesFlag = true;
            }
            else if (vm.AccessionOptionValue == "Selections")
            {
                crossesFlag = false;

            }

           

            List<ReportParameter> reportparameters = new List<ReportParameter>();
            reportparameters.Add(new ReportParameter(paramGenusId, vm.GenusId.ToString()));
            reportparameters.Add(new ReportParameter(paramYear, vm.Year));
            reportparameters.Add(new ReportParameter(paramReportTitle, reportTitle));
            reportparameters.Add(new ReportParameter(paramCrossesFlag, crossesFlag.HasValue ? crossesFlag.Value.ToString() : null ));
            reportparameters.Add(new ReportParameter(paramInternalFlag, internalFlag.HasValue ? internalFlag.Value.ToString() : null));

            return GetReportViewModel(reportparameters, reportName, reportTitle);

        }

        public static ReportViewModel ToReportViewModel(this MapInventoryReportViewModel vm, string reportName, string reportTitle)
        {
            //TODO ?: Create report attribute for Report View models and use reflection to build the report params dynamically
            string paramMapId = "MapId";
            string paramMapYear = "Year";
            string paramMapName = "MapName";
            string paramReportTitle = "ReportTitle";

            List<ReportParameter> reportparameters = new List<ReportParameter>();
            reportparameters.Add(new ReportParameter(paramMapId, vm.MapId.ToString()));
            reportparameters.Add(new ReportParameter(paramMapYear, vm.MapYear));
            reportparameters.Add(new ReportParameter(paramMapName, vm.MapName));
            reportparameters.Add(new ReportParameter(paramReportTitle, reportTitle));

            return GetReportViewModel(reportparameters, reportName, reportTitle);

        }

        public static ReportViewModel ToReportViewModel(this MapComponentFatesReportViewModel vm, string reportName, string reportTitle)
        {
            string paramMapYear = "Year";
            string paramMapIsSeedling = "MapIsSeedling";
            string paramMapGenusId = "GenusId";
            string paramReportTitle = "ReportTitle";

            List<ReportParameter> reportparameters = new List<ReportParameter>();
            reportparameters.Add(new ReportParameter(paramMapYear, vm.MapYear));
            reportparameters.Add(new ReportParameter(paramMapIsSeedling, vm.MapIsSeedling.ToString()));
            reportparameters.Add(new ReportParameter(paramMapGenusId, vm.GenusId.ToString()));
            reportparameters.Add(new ReportParameter(paramReportTitle, reportTitle));

            return GetReportViewModel(reportparameters, reportName, reportTitle);

        }

        public static ReportViewModel ToReportViewModel(this BreedingSummaryReportViewModel vm, string reportName, string reportTitle)
        {
            string paramMapGenusId = "GenusId";
            string paramReportTitle = "ReportTitle";

            List<ReportParameter> reportparameters = new List<ReportParameter>();
            reportparameters.Add(new ReportParameter(paramMapGenusId, vm.GenusId.ToString()));
            reportparameters.Add(new ReportParameter(paramReportTitle, reportTitle));
                
            return GetReportViewModel(reportparameters, reportName, reportTitle);

        }


        public static ReportViewModel ToReportViewModel(this SelectionSummaryReport vm, string reportName, string reportTitle)
        {
            string paramGenotypeId = "GenotypeId";
            string paramReportTitle = "ReportTitle";

            List<ReportParameter> reportparameters = new List<ReportParameter>();
            reportparameters.Add(new ReportParameter(paramGenotypeId, vm.GenotypeId.ToString()));
            reportparameters.Add(new ReportParameter(paramReportTitle, reportTitle));

            return GetReportViewModel(reportparameters, reportName, reportTitle);

        }

        public static string GetValueOrBlank(this string s)
        {
            return (s == null) ? string.Empty : s;
        }
        

    }
}
