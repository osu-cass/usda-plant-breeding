using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Usda.PlantBreeding.Site.Models;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Site.Models.Translators;
using Usda.PlantBreeding.Site.Models.Helpers;
using Usda.PlantBreeding.Core.Interfaces;
using Usda.PlantBreeding.Core.Infrastructure;
using Usda.PlantBreeding.Core.Models;
using BSGUtils;
using Usda.PlantBreeding.Site.CustomAttributes;
using Newtonsoft.Json.Converters;

namespace Usda.PlantBreeding.Site.Areas.Maps.Controllers
{
    [ActionFilters.AuthActionFilters]
    public class PhenotypeEntryController : Controller
    {
        private IPlantBreedingRepo m_repo;
        private IPhenotypeEntryUOW p_repo;
        private IMapUOW map_repo;

        public PhenotypeEntryController() : this(new PlantBreedingRepo()) { }

        public PhenotypeEntryController(IPlantBreedingRepo repo)
        {
            m_repo = repo;
            map_repo = new MapUow(repo);
            p_repo = new PhenotypeEntryUOW(repo);
        }

        public ActionResult Index(int? mapId, string year)
        {
            ActionResult result = null;

            // year has a string replace and default value in the view, this handles that. TODO: Find a better way. 
            if (!mapId.HasValue || year == "yearPlaceHolder" || year == null)
            {
                result = RedirectToAction("Index", "Default", new { area = "Maps" });
            }
            else
            {

                var vm = p_repo.GetPhenotypeEntryVM(mapId.Value, year);

                result = View(vm);
            }
            return result;
        }

        public ActionResult Edit(int? mapId, string year)
        {

            // year has a string replace and default value in the view, this handles that. TODO: Find a better way. 
            if (!mapId.HasValue || year == "yearPlaceHolder" || year == null)
            {
                return RedirectToAction("Index", "Default", new { area = "Maps" });
            }
            var map = m_repo.GetMap(mapId.Value);
            
            if(map == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vm = p_repo.GetPhenotypeEntryVM(mapId.Value, year);
            return View(vm);
        }

        [HttpGet]
        public ActionResult GetPhenotypeRows(int? genotypeId, int? mapId, string year )
        {
            PhenotypeRowsViewModel rows = null;

            if (genotypeId.HasValue)
            {
                rows = p_repo.GetGenotypeRows(genotypeId.Value);
            }else if(mapId.HasValue && !year.IsNullOrWhiteSpace())
            {
                rows = p_repo.GetPhenotypeMapRows(mapId.Value, year);
            }

            if(rows == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                JsonNetResult jsonNetResult = new JsonNetResult();
                jsonNetResult.Data = rows;
                jsonNetResult.SerializerSettings.DateFormatString = "yyyy-MM-dd";
                return jsonNetResult;

            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, e.Message);
            }

        }

        [HttpPost] 
        public ActionResult SaveAnswerJson(AnswerViewModel answer)
        {
            p_repo.SaveAnswer(answer);
            return Json(answer);
        }

        [HttpPost]
        public JsonResult SaveCommentsJson(MapComponentSummaryVM viewModel)
        {
            p_repo.SaveComments(viewModel);
            return Json(viewModel);
        }

        [HttpPost]
        public JsonResult SavePlantNum(MapComponentSummaryVM viewModel)
        {
            p_repo.SavePlantNum(viewModel);
            return Json(viewModel);

        }

        [HttpPost]
        public JsonResult SaveRowNum(MapComponentSummaryVM viewModel)
        {
            p_repo.SaveRowNum(viewModel);
            return Json(viewModel);
        }

        [HttpPost]
        public JsonResult SaveDateCreated(MapComponentSummaryVM viewModel)
        {
            p_repo.SaveCreatedDate(viewModel);
            return Json(viewModel);
        }


        [HttpPost]
        public JsonResult SaveFatesJson(int mcrId, int[] fates)
        {
            IEnumerable<Fate> fatesToUpdate = null;
            if(fates != null)
            {
                fatesToUpdate = m_repo.GetFates().Where(f => fates.Any(i => i == f.Id));
            }
            m_repo.UpdateMapComponentFates(fatesToUpdate, mcrId);
            return Json(new { Error = false });
        }

         
        public JsonResult SearchAccessions(string textVal, int genusId, int mapId)
        {
            textVal = textVal.TrimAndRemoveDoubleSpaces();
            if (string.IsNullOrEmpty(textVal) || textVal.Length < 1)
            {
                return Json(new Object[] { }, JsonRequestBehavior.AllowGet);
            }
            int recordsToReturn = Properties.Settings.Default.RecordsToReturn;
            IEnumerable<DataListViewModel> genotypeList = p_repo.SearchAccessions(textVal, genusId, mapId, true, recordsToReturn);
            return Json(genotypeList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateSeedlingRow(SeedlingViewModel seedlingViewModel)
        {
            try
            {
                var mapComponentSummaryVM = p_repo.CreateSeedlingSelection(seedlingViewModel);
                JsonNetResult jsonNetResult = new JsonNetResult();
                jsonNetResult.Data = mapComponentSummaryVM;
                jsonNetResult.SerializerSettings.DateFormatString = "yyyy-MM-dd";
                return jsonNetResult;

            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, e.Message);
            }
        }

        public ActionResult PhenotypeEntryYear(int? mapId)
        {
            if (!mapId.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MapBuilderViewModel vm = map_repo.GetMapBuilderViewModel(mapId.Value);
            ViewBag.Years = vm.Years;

            if (vm == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(vm);
        }
    }
}