using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using System.Linq;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Site.Models;
using Usda.PlantBreeding.Site.Models.Translators;
using System;
using Usda.PlantBreeding.Core.Interfaces;
using Usda.PlantBreeding.Core.Infrastructure;
using Usda.PlantBreeding.Core.Models;
using BSGUtils;

using Usda.PlantBreeding.Core.Translations;

namespace Usda.PlantBreeding.Site.Areas.Accessions.Controllers
{
    [ActionFilters.AuthActionFilters]
    public class CrossPlanningController : Controller
    {
        private IPlantBreedingRepo m_repo;
        private ICrossPlanUOW c_repo;

        public CrossPlanningController() : this(new PlantBreedingRepo()) { }

        public CrossPlanningController(IPlantBreedingRepo repo)
        {
            m_repo = repo;
            c_repo = new CrossPlanUOW(repo);
        }

        [HttpGet]
        [ActionFilters.GenusActionFilter]
        public ActionResult Index(string year, int? genusId)
        {
            if (genusId == null) {
                genusId = SessionManager.GetGenusId().Value;
            }
            Genus genus = m_repo.GetGenus(genusId.Value);
            //TODO: check for valid year
            if(year.IsNullOrWhiteSpace())
            {
                year = DateTime.Now.Year.ToString();
            }
            CrossPlanIndexViewModel crossPlanIndex = c_repo.GetIndex(year, genusId.Value);
            ViewBag.GenusId = genusId;
            ViewBag.CrossTypes = crossPlanIndex.CrossTypes;
            return View(crossPlanIndex);
        }

        /// <summary>
        /// Creates a new cross plan row.
        /// </summary>
        /// <param name="cpvm">
        /// View model for new cross plan row.
        /// </param>
        /// <returns>
        /// Index view for cross plan.
        /// </returns>
        [HttpPost]
        [ActionFilters.GenusActionFilter]

        public ActionResult Index(CrossPlanIndexViewModel cpvm)
        {

            if (ModelState.IsValid)
            {
                c_repo.SaveIndex(cpvm);
                int genusId = SessionManager.GetGenusId().Value;
                string year = cpvm.CurrentYear;
                 cpvm = c_repo.GetIndex(year, genusId);

            }

            return Json(cpvm);
            
        }

        public ActionResult AddRow(string year, int genusId)
        {
            ActionResult view = null;
            ViewBag.CrossTypes = c_repo.GetCrossTypesList(genusId);
            ViewBag.GenusId = genusId;
            try
            {
                CrossPlanViewModel plan = c_repo.CreateNextCrossPlan(year, genusId);
                view = PartialView("_CrossPlanRow", plan);
            }
            catch (CrossPlanException e)
            {
                view = Json(new { Error = true, Message = e.Message });
            }

            return view;
        }

        public ActionResult OrderByDefault(CrossPlanIndexViewModel cpvm)
        {
            var plans = cpvm.CrossPlans;
            plans = c_repo.DefaultOrder(plans).ToList();
            cpvm.CrossPlans = plans;

            return Json(cpvm);
        }
        public ActionResult OrderByParent(CrossPlanIndexViewModel cpvm)
        {
            var plans = cpvm.CrossPlans;
            plans = c_repo.OrderByParents(plans).ToList();
            cpvm.CrossPlans = plans.OrderBy(t => t.FemaleParentName).ToList();

            return Json(cpvm);
        }

        public ActionResult CopyPlanToYear(int id)
        {

            bool error = false;
            string message = string.Empty;
            try
            {
                var crossplan = c_repo.GetCrossPlan(id);
                c_repo.CopyToNewYear(crossplan);
            }
            catch (CrossPlanException e)
            {
                error = true;
                message = e.Message;
            }

            return Json(new { Error = error, Message = message });
        }
        [HttpPost]
        public ActionResult RemovePlan(int id)
        {
            bool error = false;
            string message = string.Empty;
            try
            {
                var crossplan = c_repo.GetCrossPlan(id);
                c_repo.Delete(crossplan);
            }
            catch (CrossPlanException e)
            {
                error = true;
                message = e.Message;
            }

            return Json(new { Error = error, Message = message });
        }
        [HttpPost]
        public ActionResult SavePlan(CrossPlanViewModel cpvm)
        {
            bool error = false;
            string message = string.Empty;
            try
            {
                if(cpvm.DateCreated == DateTime.MinValue && cpvm.FemaleParentId != null && cpvm.MaleParentId != null)
                {
                    cpvm.DateCreated = DateTime.Now;
                    message = cpvm.DateCreated.ToShortDateString();
                }
                c_repo.Save(cpvm);

            }
            catch (CrossPlanException e)
            {
                error = true;
                message = e.Message;
            }
            return Json(new { Error = error, Message = message });
        }
      

        public JsonResult GetParents(int? genotypeId)
        {
            ParentViewModel pvm = new ParentViewModel();
            if (genotypeId.HasValue)
            {
                pvm = c_repo.GetParents(genotypeId.Value);
            }

            return Json(pvm, JsonRequestBehavior.AllowGet);
        }



       
        /// <summary>
        /// Autocomplete search for genotypes with original name (or name) containing given term.
        /// </summary>
        /// <param name="term">
        /// Term to search for in original name (or name) of genotypes.
        /// </param>
        /// <returns>
        /// Returns JSON array of genotypes(name, original name, id) whose original name (or name) contains given term.
        /// </returns>
        public JsonResult SearchAccessions(string term, int genusId)
        {
            ViewBag.GenusId = genusId;
            term = term.TrimAndRemoveDoubleSpaces();
            if (string.IsNullOrEmpty(term) || term.Length < 1)
            {
                return Json(new Object[] { }, JsonRequestBehavior.AllowGet);
            }
            int recordsToReturn = Properties.Settings.Default.RecordsToReturn;
            IEnumerable<DataListViewModel> genotypeList = c_repo.SearchAccessions(term, genusId, recordsToReturn);
            return Json(genotypeList, JsonRequestBehavior.AllowGet);
        }

  

        [HttpPost]
        public ActionResult CreateFamily(int id)
        {
            bool error = false;
            string message = string.Empty;
            try
            {
                c_repo.SaveFamily(id);

            }
            catch (CrossPlanException e)
            {
                error = true;
                message = e.Message;
            }

            return Json(new { Error = error, Message = message });
        }


        [ActionFilters.GenusActionFilter]
        private void SetViewBagForPlant(AccessionViewModel accession)
        {
            // TODO: use accession unit of work methods
           
            IEnumerable<SelectListItem> crossTypes = m_repo
                .GetCrossTypes()
                .Where(c => !c.Retired || c.Id == accession.FamilyCrossTypeId)
                .Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                    Disabled = c.Retired
                });
            ViewBag.CrossTypeId = crossTypes;
        }

        
        //Creates reciprocals of all cross plans that are not reciprocals
        public ActionResult GenerateReciprocals(string year, int genusId)
        {
            if (year == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.GenusId = genusId;
            ViewBag.CrossTypes = c_repo.GetCrossTypesList(genusId);

            IEnumerable<CrossPlanViewModel> newPlans;
            try
            {
                newPlans = c_repo.GenerateReciprocals(year, genusId);
            }
            catch (CrossPlanException e)
            {
                return Json(new { Error = true, Message = e.Message });
            }

            return PartialView("_CrossPlanRows", newPlans);
        }
    }
}