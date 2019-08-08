using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Usda.PlantBreeding.Data.DataAccess;
using System.Net;
using Usda.PlantBreeding.Data.Models;
using System.Linq;
using Usda.PlantBreeding.Core.Models;
using Usda.PlantBreeding.Core.Interfaces;
using Usda.PlantBreeding.Core.Infrastructure;
using BSGUtils;
using Usda.PlantBreeding.Core.Models.Reporting;
using System.Threading;

namespace Usda.PlantBreeding.Site.Areas.Reporting.Controllers
{
    [ActionFilters.AuthActionFilters]
    public class DefaultController : Controller
    {
         private IPlantBreedingRepo m_repo;
         private IReportingUOW r_repo;
        public DefaultController() : this(new PlantBreedingRepo()) { }

        public DefaultController(IPlantBreedingRepo repo)
        {
            m_repo = repo;
            r_repo = new ReportingUOW(repo);
        }


        public ActionResult ReportView()
        {
            int trynum = 1;
            while (trynum <= 2)
            {
                try
                {
                    ReportViewModel report = TempData["ReportViewModel"] as ReportViewModel;

                    if (report == null)
                    {
                        return RedirectToAction("Index");

                    }

                    ReportViewer reportviewer = new ReportViewer();
                    reportviewer.ProcessingMode = ProcessingMode.Remote;

                    reportviewer.ServerReport.ReportServerUrl = new Uri(Properties.Settings.Default.ReportUrl);
                    reportviewer.ServerReport.ReportPath = Properties.Settings.Default.ReportPath + report.ReportName;

                    reportviewer.ShowPageNavigationControls = true;
                    reportviewer.ShowParameterPrompts = false;
                    reportviewer.Height = 750;
                    reportviewer.Width = 1400;
                    reportviewer.ZoomPercent = 90;
                    reportviewer.KeepSessionAlive = true;

                    IEnumerable<ReportParameter> parameters = report.parameters;
                    if (parameters != null)
                    {
                        foreach (var val in parameters)
                        {
                            reportviewer.ServerReport.SetParameters(val);
                        }
                    }
                    ViewBag.ReportViewer = reportviewer;
                    ViewBag.Title = report.ReportTitle;
                    var view = View();
                    if(view == null)
                    {
                        Thread.Sleep(10000);
                        trynum++;
                    }
                    else
                    {
                        return view;
                    }
                }
                catch (NullReferenceException e)
                {
                    //wait for 10 seconds before trying again
                    Thread.Sleep(10000);
                    trynum++;
                }
            }
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Accessions()
        {
            ViewBag.Genus = new SelectList(m_repo.GetAllGenera().Where(t => t.Retired == false).OrderBy(t => t.Name), "Id", "Value",SessionManager.GetGenusId());
            string year = DateTime.Now.Year.ToString();
            AccessionReportViewModel vm = r_repo.GetAccessionReportViewModel(SessionManager.GetGenusId(), year);

            return View(vm);
        }

        [HttpPost]
        public ActionResult Accessions(AccessionReportViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Genus = new SelectList(m_repo.GetAllGenera().Where(t => t.Retired == false).OrderBy(t => t.Name), "Id", "Value", vm.GenusId);
                return View(vm);
            }

            ReportViewModel reportVM = r_repo.GetReportForAccession(vm);
            TempData["ReportViewModel"] = reportVM;

            return RedirectToAction("ReportView");

        }

        [HttpGet]
        public ActionResult AccessionParents()
        {
            ViewBag.Genus = new SelectList(m_repo.GetAllGenera().Where(t => t.Retired == false).OrderBy(t => t.Name), "Id", "Value", SessionManager.GetGenusId());
            AccessionReportViewModel vm = r_repo.GetAccessionReportViewModel(SessionManager.GetGenusId(), "");

            return View(vm);
        }

        [HttpPost]
        public ActionResult AccessionParents(AccessionReportViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Genus = new SelectList(m_repo.GetAllGenera().Where(t => t.Retired == false).OrderBy(t => t.Name), "Id", "Value", vm.GenusId);
                return View(vm);
            }

            ReportViewModel reportVM = r_repo.GetReportForAccessionParents(vm);
            TempData["ReportViewModel"] = reportVM;

            return RedirectToAction("ReportView");

        }


        [HttpGet]
        public ActionResult AccessionsCrossesMade(string year, int? genusId)
        {
            if (!genusId.HasValue)
            {
                genusId = SessionManager.GetGenusId();
            }

            if (year == null || year == string.Empty)
            {
                year = DateTime.Now.Year.ToString();
            }

            ViewBag.genus = new SelectList(m_repo.GetAllGenera().Where(t => t.Retired == false).OrderBy(t => t.Name), "Id", "Value", genusId);

            AccessionReportViewModel vm = r_repo.GetAccessionsCrossesMadeReportViewModel(genusId, year);

            return View(vm);
        }

        [HttpPost]
        public ActionResult AccessionsCrossesMade(AccessionReportViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Genus = new SelectList(m_repo.GetAllGenera().Where(t => t.Retired == false).OrderBy(t => t.Name), "Id", "Value", vm.GenusId);
                return View(vm);
            }

            ReportViewModel reportVM = r_repo.GetReportForAccession(vm);
            TempData["ReportViewModel"] = reportVM;

            return RedirectToAction("ReportView");          

        }
        [HttpGet]
        public ActionResult Distributions(string year, int? locationId, int? genotypeId)
        {
            if (year == null || year == string.Empty)
            {
                year = DateTime.Now.Year.ToString();
            }
            DistributionReportViewModel vm = r_repo.GetDistributionReportViewModel(year, locationId, genotypeId);
            return View(vm);
        }
        [HttpPost]
        public ActionResult Distributions(DistributionReportViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }

            ReportViewModel reportVM = r_repo.GetReportForDistributions(vm);
            TempData["ReportViewModel"] = reportVM;

            return RedirectToAction("ReportView");
        }
        [HttpGet]
        public ActionResult CrossPlansByYear(string year, int? genus)
        {
            if (!genus.HasValue)
            {
                genus =SessionManager.GetGenusId();
            }

            if (year == null)
            {
                year =  DateTime.Now.Year.ToString();
            }

            CrossPlanReportViewModel vm = r_repo.GetCrossPlanReportViewModel(genus, year);
            ViewBag.genus = new SelectList(m_repo.GetAllGenera(), "Id", "Value", vm.GenusId);

            return View(vm);
        }

        [HttpPost]
        public ActionResult CrossPlansByYear(CrossPlanReportViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.genus = new SelectList(m_repo.GetAllGenera(), "Id", "Value", vm.GenusId);
                return View(vm);
            }

            ReportViewModel reportVM = r_repo.GetReportForCrossPlans(vm);
            TempData["ReportViewModel"] = reportVM;

            return RedirectToAction("ReportView");
        }

        [HttpGet]
        public ActionResult CrossPlanLocation(string year, int? genus)
        {
            if (!genus.HasValue)
            {
                genus =SessionManager.GetGenusId();
            }

            if (year == null)
            {
                year = DateTime.Now.Year.ToString();
            }

            CrossPlanLocationReportViewModel vm = r_repo.GetCrossPlanLocationReportViewModel(genus, year);
            ViewBag.genus = new SelectList(m_repo.GetAllGenera(), "Id", "Value", vm.GenusId);

            return View(vm);
        }

        [HttpPost]
        public ActionResult CrossPlanLocation(CrossPlanLocationReportViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.genus = new SelectList(m_repo.GetAllGenera(), "Id", "Value", vm.GenusId);
                return View(vm);
            }

            ReportViewModel reportVM = r_repo.GetReportForCrossPlanLocation(vm);
            TempData["ReportViewModel"] = reportVM;

            return RedirectToAction("ReportView");
        }

        [HttpGet]
        public ActionResult BreedingSummary()
        {
            ViewBag.Genus = new SelectList(m_repo.GetAllGenera().Where(t => t.Retired == false).OrderBy(t => t.Name), "Id", "Value",SessionManager.GetGenusId());
            string year = DateTime.Now.Year.ToString();
            BreedingSummaryReportViewModel vm = r_repo.GetBreedingSummaryReportViewModel(SessionManager.GetGenusId());

            return View(vm);
        }

        [HttpPost]
        public ActionResult BreedingSummary(BreedingSummaryReportViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Genus = new SelectList(m_repo.GetAllGenera().Where(t => t.Retired == false).OrderBy(t => t.Name), "Id", "Value", vm.GenusId);
                return View(vm);
            }

            ReportViewModel reportVM = r_repo.GetReportForBreedingSummaryReportViewModel(vm);
            TempData["ReportViewModel"] = reportVM;

            return RedirectToAction("ReportView");
        }

        [HttpGet]
        public ActionResult MapView(int? mapId, string year = null)
        {
            MapViewReportViewModel reportVM = r_repo.GetMapViewReportViewModel(mapId, year);

            return View(reportVM);
        }

        [HttpPost]
        public ActionResult MapView(MapViewReportViewModel mapView)
        {
            if (!ModelState.IsValid)
            {
                return View(mapView);
            }
            ReportViewModel reportVM = r_repo.GetReportForMapView(mapView);
            TempData["ReportViewModel"] = reportVM;

            return RedirectToAction("ReportView");
        }

        [HttpGet]
        public ActionResult MapComponents(int? mapId, string year = null)
        {
            MapComponentReportViewModel reportVM = r_repo.GetMapComponentReportViewModel(mapId, year);

            return View(reportVM);
        }

        [HttpPost]
        public ActionResult MapComponents(MapComponentReportViewModel mapView)
        {
            if (!ModelState.IsValid)
            {
                return View(mapView);
            }

            ReportViewModel reportVM = r_repo.GetReportForMapComponent(mapView);
            TempData["ReportViewModel"] = reportVM;

            return RedirectToAction("ReportView");
        }

        [HttpGet]
        public ActionResult MapComponentFates()
        {

            MapComponentFatesReportViewModel reportVM = r_repo.GetMapComponentFatesReportViewModel( SessionManager.GetGenusId());

            ViewBag.Genus = new SelectList(m_repo.GetAllGenera().Where(t => t.Retired == false).OrderBy(t => t.Name), "Id", "Value",reportVM.GenusId);


            return View(reportVM);
        }

        [HttpPost]
        public ActionResult MapComponentFates(MapComponentFatesReportViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Genus = new SelectList(m_repo.GetAllGenera().Where(t => t.Retired == false).OrderBy(t => t.Name), "Id", "Value", vm.GenusId);
                return View(vm);
            }

            ReportViewModel reportVM = r_repo.GetReportForMapComponentFates(vm);
            TempData["ReportViewModel"] = reportVM;

            return RedirectToAction("ReportView");
        }


        [HttpGet]
        public ActionResult MapInventory(int? mapId, string year = null)
        {
            if (!mapId.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            }

            MapInventoryReportViewModel reportVM = r_repo.GetMapInventoryReportViewModel(mapId, year);

            return View(reportVM);
        }

        [HttpPost]
        public ActionResult MapInventory(MapInventoryReportViewModel mapInventory)
        {
            if (!ModelState.IsValid)
            {
                return View(mapInventory);
            }

            ReportViewModel reportVM = r_repo.GetReportForMapInventory(mapInventory);
            TempData["ReportViewModel"] = reportVM;

            return RedirectToAction("ReportView");
        }


        [HttpGet]
        public ActionResult PhenotypeEntry(int? mapId, string year = null)
        {
            PhenotypeReportViewModel reportVM = r_repo.GetPhenotypeReportViewModel(mapId, year);
            
            return View(reportVM);
        }

        [HttpPost]
        public ActionResult PhenotypeEntry(PhenotypeReportViewModel mapView)
        {
            if (!ModelState.IsValid)
            {
                return View(mapView);
            }

            ReportViewModel reportVM = r_repo.GetReportForPhenotype(mapView);
            TempData["ReportViewModel"] = reportVM;

            return RedirectToAction("ReportView");
        }

        [HttpGet]
        public ActionResult SelectionSummary(int? genotypeId)
        {
            if (!genotypeId.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SelectionSummaryReport report = new SelectionSummaryReport
            {
                GenotypeId = genotypeId.Value
            };

            ReportViewModel reportVM = r_repo.GetSelectionSummaryReport(report);
            TempData["ReportViewModel"] = reportVM;

            return RedirectToAction("ReportView");
        }

    }
}