using System.Linq;
using Usda.PlantBreeding.Core.Interfaces;
using Usda.PlantBreeding.Core.Models;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Core.Translations;
using System;
using Usda.PlantBreeding.Core.Models.Reporting;

namespace Usda.PlantBreeding.Core.Infrastructure
{
    public class ReportingUOW : IReportingUOW
    {
        private IPlantBreedingRepo u_repo;
        public ReportingUOW() : this(new PlantBreedingRepo()) { }
        public ReportingUOW(IPlantBreedingRepo repo)
        {
            u_repo = repo;
        }

        #region MapViewModels
        public MapViewReportViewModel GetMapViewReportViewModel(int? mapId, string year = null)
        {
            MapViewReportViewModel vm = new MapViewReportViewModel();
            if (mapId.HasValue || year != null)
            {
                Map map = u_repo.GetMap(mapId.Value);
                if (year == null && map != null)
                {
                    year = map.Years.Max(t => int.Parse(t.Year)).ToString();
                }

                vm.MapId = map.Id;
                vm.MapName = map.Name;
                vm.MapFullName = map.FullName;
                vm.MapYear = year;
                vm.MapPlantedYear = map.PlantingYear; 
                vm.MapIsSeedling = map.IsSeedlingMap;
                if (map.IsSeedlingMap)
                {
                    vm.SeedlingComponentValue = "Planted";
                }
            }

            return vm;
        }

        public MapComponentReportViewModel GetMapComponentReportViewModel(int? mapId, string year = null)
        {
            MapComponentReportViewModel vm = new MapComponentReportViewModel();
            if (mapId.HasValue || year != null)
            {
                Map map = u_repo.GetMap(mapId.Value);
                if (year == null && map != null)
                {
                    year = map.Years.Max(t => int.Parse(t.Year)).ToString();
                }

                vm.MapId = map.Id;
                vm.MapName = map.Name;
                vm.MapFullName = map.FullName;
                vm.MapYear = year;
                vm.MapIsSeedling = map.IsSeedlingMap;
                vm.MapPlantedYear = map.PlantingYear; 
                vm.Virus1Label = map.Genus.VirusLabel1;
                vm.Virus2Label = map.Genus.VirusLabel2;
                vm.Virus3Label = map.Genus.VirusLabel3;
                vm.Virus4Label = map.Genus.VirusLabel4;
                if (map.IsSeedlingMap)
                {
                    vm.SeedlingComponentValue = "Planted";
                }
            }

            return vm;
        }

        public MapInventoryReportViewModel GetMapInventoryReportViewModel(int? mapId, string year = null)
        {
            MapInventoryReportViewModel vm = new MapInventoryReportViewModel();
            if (mapId.HasValue || year != null)
            {
                Map map = u_repo.GetMap(mapId.Value);
                if (year == null && map != null)
                {
                    year = map.Years.Max(t => int.Parse(t.Year)).ToString();
                }

                vm.MapId = map.Id;
                vm.MapName = map.Name;
                vm.MapFullName = map.FullName;
                vm.MapPlantedYear = map.PlantingYear; 
                vm.MapYear = year;
                vm.MapIsSeedling = map.IsSeedlingMap;
            }

            return vm;
        }
        public MapComponentFatesReportViewModel GetMapComponentFatesReportViewModel(int? genusId)
        {
            MapComponentFatesReportViewModel vm = new MapComponentFatesReportViewModel();
            vm.MapYear = DateTime.Now.Year.ToString();
            vm.MapIsSeedling = true;
            if (genusId.HasValue)
            {
                Genus genus = u_repo.GetGenus(genusId.Value);
                vm.GenusId = genus.Id;
                vm.GenusName = genus.Name;
      
            }


            return vm;
        }


        public PhenotypeReportViewModel GetPhenotypeReportViewModel(int? mapId, string year = null)
        {
            PhenotypeReportViewModel vm = new PhenotypeReportViewModel();
            if (mapId.HasValue || year != null)
            {
                Map map = u_repo.GetMap(mapId.Value);
                if (year == null && map != null)
                {
                    year = map.Years.Max(t => int.Parse(t.Year)).ToString();
                }

                vm.MapId = map.Id;
                vm.MapName = map.Name;
                vm.MapFullName = map.FullName;
                vm.MapPlantedYear = map.PlantingYear; 
                vm.MapYear = year;
            }

            return vm;
        }

        #endregion

        #region AccessionModels
        public AccessionReportViewModel GetAccessionReportViewModel(int? genusId, string year)
        {
            AccessionReportViewModel vm = new AccessionReportViewModel();
            if (genusId.HasValue)
            {
                Genus genus = u_repo.GetGenus(genusId.Value);
                vm.GenusId = genus.Id;
                vm.GenusName = genus.Name;
            }
            vm.AccessionOptionValue = "Both";
            vm.OriginOptionValue = "Both";
            vm.Year = year;
            vm.isPlanning = false;

            return vm;
        }

        public AccessionReportViewModel GetAccessionsCrossesMadeReportViewModel(int? genusId, string year)
        {
            AccessionReportViewModel vm = new AccessionReportViewModel();
            if (genusId.HasValue)
            {
                Genus genus = u_repo.GetGenus(genusId.Value);
                vm.GenusId = genus.Id;
                vm.GenusName = genus.Name;
            }
            vm.AccessionOptionValue = "Populations";
            vm.OriginOptionValue = "Internal";
            vm.Year = year;
            vm.isPlanning = true;

            return vm;
        }
        public DistributionReportViewModel GetDistributionReportViewModel(string year, int? locationId, int? genotypeId)
        {
            DistributionReportViewModel vm = new DistributionReportViewModel();
            vm.Year = year;
            vm.GenotypeId = genotypeId ?? null;
            vm.LocationId = locationId ?? null;
            return vm;
        }
        public CrossPlanReportViewModel GetCrossPlanReportViewModel(int? genusId, string year)
        {
            CrossPlanReportViewModel vm = new CrossPlanReportViewModel();
            if (genusId.HasValue)
            {
                Genus genus = u_repo.GetGenus(genusId.Value);
                vm.GenusId = genus.Id;
                vm.GenusName = genus.Name;
            }
            vm.Year = year;

            return vm;
        }

        public CrossPlanLocationReportViewModel GetCrossPlanLocationReportViewModel(int? genusId, string year)
        {
            CrossPlanLocationReportViewModel vm = new CrossPlanLocationReportViewModel();
            if (genusId.HasValue)
            {
                Genus genus = u_repo.GetGenus(genusId.Value);
                vm.GenusId = genus.Id;
                vm.GenusName = genus.Name;
                vm.Virus1Label = genus.VirusLabel1;
                vm.Virus2Label = genus.VirusLabel2;
                vm.Virus3Label = genus.VirusLabel3;
                vm.Virus4Label = genus.VirusLabel4;
            }

            vm.Year = year;

            return vm;
        }

        public BreedingSummaryReportViewModel GetBreedingSummaryReportViewModel(int? genusId)
        {
            BreedingSummaryReportViewModel vm = new BreedingSummaryReportViewModel();
            if (genusId.HasValue)
            {
                Genus genus = u_repo.GetGenus(genusId.Value);
                vm.GenusId = genus.Id;
                vm.GenusName = genus.Name;
            }

            return vm;
        }


        #endregion

        #region ReportViewModel

        private string GetMapOptionsTitle(string optionValue)
        {
            string optionsTitle = string.Empty ;
            if (optionValue == "Both")
            {
                optionsTitle = "All";
            }
            else if (optionValue == "SelectionsMade")
            {
                optionsTitle = "Selections Made";
            }
            else
            {
                optionsTitle = optionValue;
            }

            return optionsTitle;

        }
        public ReportViewModel GetReportForMapView(MapViewReportViewModel vm)
        {
            string reportName = Properties.Settings.Default.ReportNameMapView;
            string reportTitle = string.Empty;
            string optionsTitle = GetMapOptionsTitle(vm.SeedlingComponentValue);

            if (vm.MapIsSeedling)
            {
                reportTitle = string.Format("{0}: Map View {1} {2}", vm.MapFullName, vm.MapYear, optionsTitle);
            }
            else
            {
                reportTitle = string.Format("{0}: Map View {1} ", vm.MapFullName, vm.MapYear);
            }

            return vm.ToReportViewModel(reportName, reportTitle);
        }
        public ReportViewModel GetReportForMapComponent(MapComponentReportViewModel vm)
        {
            string reportName = Properties.Settings.Default.ReportNameMapComponent;
            string reportTitle = string.Empty;
            string optionsTitle = GetMapOptionsTitle(vm.SeedlingComponentValue);
            if (vm.MapIsSeedling)
            {
                reportTitle = string.Format("{0}: Map Components {1} {2}", vm.MapFullName, vm.MapYear, optionsTitle);
            }
            else
            {
                reportTitle = string.Format("{0}: Map Components {1} ", vm.MapFullName, vm.MapYear);
            }

            return vm.ToReportViewModel(reportName, reportTitle);
        }
        public ReportViewModel GetReportForPhenotype(PhenotypeReportViewModel vm)
        {
            string reportName = Properties.Settings.Default.ReportNamePhenotype;
            string reportTitle = string.Format("{0}: Phenotype Entry {1}", vm.MapFullName, vm.MapYear);
            return vm.ToReportViewModel(reportName, reportTitle);
        }
        public ReportViewModel GetReportForCrossPlanLocation(CrossPlanLocationReportViewModel vm)
        {
            string reportName = Properties.Settings.Default.ReportNameCrossPlanLocation;
            if (vm.GenusId.HasValue)
            {
                vm.GenusValue = u_repo.GetGenus(vm.GenusId.Value).Value;
            }

            string reportTitle = string.Format("{0} {1} Cross Locations", vm.Year, vm.GenusValue);
            return vm.ToReportViewModel(reportName, reportTitle);
        }
        public ReportViewModel GetReportForDistributions(DistributionReportViewModel vm)
        {
            //string reportName = Properties.Settings.Default.ReportNameDistributions;
            string reportTitle = string.Format("{0} Distributions", vm.Year);
            return vm.ToReportViewModel(/*reportName*/"temp", reportTitle);
        }
        public ReportViewModel GetReportForCrossPlans(CrossPlanReportViewModel vm)
        {
            string reportName = Properties.Settings.Default.ReportNameCrossPlans;
            if (vm.GenusId.HasValue)
            {
                vm.GenusValue = u_repo.GetGenus(vm.GenusId.Value).Value;
            }
            string reportTitle = string.Format("{0} {1} Cross Planning", vm.Year, vm.GenusValue);
            return vm.ToReportViewModel(reportName, reportTitle);
        }

        public ReportViewModel GetReportForAccession(AccessionReportViewModel vm)
        {
            string reportName = Properties.Settings.Default.ReportNameAccession;
            vm.GenusValue = u_repo.GetGenus(vm.GenusId).Value;
            string originTitle = (vm.OriginOptionValue == "Both") ? "All Origins" : vm.OriginOptionValue + " Origins";
            string accessionTitle = (vm.AccessionOptionValue == "Both") ? "Both Types" : vm.AccessionOptionValue;
            string reportTitle = string.Empty;  

            if (vm.isPlanning)
            {
                reportTitle = vm.GenusValue + " Crosses Made " + vm.Year;
            }
            else
            {
                reportTitle = string.Format("{0} {1} Accessions {2} {3}", vm.Year, vm.GenusValue, originTitle, accessionTitle);
            }

            return vm.ToReportViewModel(reportName, reportTitle);
        }

        public ReportViewModel GetReportForAccessionParents(AccessionReportViewModel vm)
        {
            string reportName = Properties.Settings.Default.ReportNameAccessionParents;
            vm.GenusValue = u_repo.GetGenus(vm.GenusId).Value;
            string originTitle = (vm.OriginOptionValue == "Both") ? "All Origins" : vm.OriginOptionValue + " Origins";
            string accessionTitle = (vm.AccessionOptionValue == "Both") ? "Both Types" : vm.AccessionOptionValue;
            string reportTitle = string.Empty;

            if (vm.isPlanning)
            {
                reportTitle = vm.GenusValue + " Crosses Made " + vm.Year;
            }
            else
            {
                reportTitle = string.Format("{0} {1} Accession Parents {2} {3}", vm.Year, vm.GenusValue, originTitle, accessionTitle);
            }

            return vm.ToReportViewModel(reportName, reportTitle);

        }

        public ReportViewModel GetReportForMapInventory(MapInventoryReportViewModel vm)
        {
            string reportName = Properties.Settings.Default.ReportNameMapInventory;
            string reportTitle = string.Format("{0}: Map Inventory {1} ", vm.MapFullName, vm.MapYear);
            return vm.ToReportViewModel(reportName, reportTitle);
        }
        public ReportViewModel GetReportForMapComponentFates(MapComponentFatesReportViewModel vm)
        {
            string reportName = Properties.Settings.Default.ReportNameMapComponentFates;
            string mapType = (vm.MapIsSeedling) ? "Seedling" : "Non-Seedling";
            string reportTitle = string.Format("{0} {1} - Map Component Fates", vm.MapYear, mapType);
            return vm.ToReportViewModel(reportName, reportTitle);
        }

        public ReportViewModel GetReportForBreedingSummaryReportViewModel(BreedingSummaryReportViewModel vm)
        {
            string reportName = Properties.Settings.Default.ReportNameBreedingSummary;
            vm.GenusValue = u_repo.GetGenus(vm.GenusId).Value;
            string reportTitle = vm.GenusValue + " Breeding Summary";
            return vm.ToReportViewModel(reportName, reportTitle);
        }

        public ReportViewModel GetSelectionSummaryReport(SelectionSummaryReport vm)
        {
            string reportName = Properties.Settings.Default.ReportNameSelectionSummary;
            var genotype = u_repo.GetGenotype(vm.GenotypeId);

            string reportTitle = $"{genotype.Name} Phenotype Summary";
            return vm.ToReportViewModel(reportName, reportTitle);
        }


        #endregion

    }
}
