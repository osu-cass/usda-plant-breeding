using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Core.Interfaces;
using Usda.PlantBreeding.Core.Infrastructure;
using Usda.PlantBreeding.Core.Models;
using BSGUtils;
using Usda.PlantBreeding.Site.Extensions;
using System.Threading.Tasks;
using System.Web.SessionState;

namespace Usda.PlantBreeding.Site.Areas.Maps.Controllers
{
    [SessionState(SessionStateBehavior.Disabled)]
    public class MapBuilderController : Controller
    {
        private IPlantBreedingRepo m_repo;
        private IMapUOW map_repo;

        public MapBuilderController() : this(new PlantBreedingRepo()) { }

        public MapBuilderController(IPlantBreedingRepo repo)
        {
            m_repo = repo;
            map_repo = new MapUow(repo);
        }


        public ActionResult Index()
        {
            return RedirectToAction("Index", "Default");
        }
        public ActionResult MapBuilder(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MapBuilderViewModel vm = map_repo.GetMapBuilderViewModel(id.Value);
            if (vm == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Years = vm.Years;

            Genus genus = vm.Map.Genus;
            ViewBag.GenusId = vm.Map.GenusId;
            ViewBag.Virus1 = genus.VirusLabel1;
            ViewBag.Virus2 = genus.VirusLabel2;
            ViewBag.Virus3 = genus.VirusLabel3;
            ViewBag.Virus4 = genus.VirusLabel4;

            return View(vm);
        }
        [HttpPost]
        public ActionResult AddMapComponent(MapComponentViewModel mapcomp)
        {
            ActionResult result;
            Map map = m_repo.GetMap(mapcomp.MapId);
            ViewBag.GenusId = map.GenusId;
            Genus genus = map.Genus;
            ViewBag.GenusId = genus.Id;
            ViewBag.Virus1 = genus.VirusLabel1;
            ViewBag.Virus2 = genus.VirusLabel2;
            ViewBag.Virus3 = genus.VirusLabel3;
            ViewBag.Virus4 = genus.VirusLabel4;

            if (ModelState.IsValid)
            {
                map_repo.Save(mapcomp);
                result = PartialView("_MapComponentRow", mapcomp);
            }
            else
            {
                result = PartialView("_MapComponent", mapcomp);
            }

            return result;

        }

        public ActionResult MapComponents(int mapId, int row, int yearId)
        {
            Map map = m_repo.GetMap(mapId);
            ViewBag.GenusId = map.GenusId;

            List<MapComponentViewModel> mapviewmodels = map_repo.GetMapComponents(map, row, yearId).ToList();
            ViewBag.GenusId = map.GenusId;
            ViewBag.Virus1 = map.Genus.VirusLabel1;
            ViewBag.Virus2 = map.Genus.VirusLabel2;
            ViewBag.Virus3 = map.Genus.VirusLabel3;
            ViewBag.Virus4 = map.Genus.VirusLabel4;

            return PartialView("_MapComponents", mapviewmodels);
        }

        public ActionResult MapComponentsAddYear(int? mapId, int? newyear)
        {
            Years y;
            if (!mapId.HasValue || !newyear.HasValue)
            {
                return Json(new { Error = true, Message = "Bad Request" });
            }

            try
            {
                y = map_repo.GetNewMapComponentYears(mapId.Value, newyear.Value);

            }
            catch (MapException e)
            {
                return Json(new { Error = true, Message = e.Message });
            }

            return Json(new { Error = false, YearsId = y.Id.ToString(), Year = y.Year });
        }


        //public async Task<JsonResult> UpdateMapComponent(MapComponentViewModel mapcomp)
        //{
        //    var result = Json(new { Error = false, Message = "" });
        //    if (!ModelState.IsValid)
        //    {
        //        result = Json(new { Error = true, Message = "Invalid" });
        //    }
        //    else
        //    {
        //        try
        //        {
        //            await Task.Run(() =>  map_repo.Save(mapcomp));
        //        }
        //        catch (MapException e)
        //        {
        //            result = Json(new { Error = true, Message = e.Message });
        //        }

        //    }
        //    return result;
           
        //}

        public JsonResult UpdateMapComponent(MapComponentViewModel mapcomp)
        {
            var result = Json(new { Error = false, Message = "" });
            if (!ModelState.IsValid)
            {
                result = Json(new { Error = true, Message = "Invalid" });
            }
            else
            {
                try
                {
                    map_repo.Save(mapcomp);
                }
                catch (MapException e)
                {
                    result = Json(new { Error = true, Message = e.Message });
                }

            }
            return result;

        }

        public JsonResult UpdateMapComponentGenotype(MapComponentViewModel mapcomp)
        {
            var result = Json(new { Error = false, Message = "" });

            if (mapcomp.Id <= 0)
            {
                result = Json(new { Error = true, Message = "Invalid" });
            }
            else
            {
                try
                {
                    map_repo.UpdateMapComponentGenotype(mapcomp.Id, mapcomp.GenotypeId);
                }
                catch (MapException e)
                {
                    result =  Json(new { Error = true, Message = e.Message });
                }

            }

            return result;
        }

        public JsonResult DeleteMapComponentYear(int? id, int? yearId)
        {

            if (!id.HasValue || !yearId.HasValue)
            {
                return Json(new { Error = true, Message = "Bad Request" });
            }
            try
            {
                map_repo.DeleteMapComponentForYear(id.Value, yearId.Value);
            }
            catch (MapException e)
            {
                return Json(new { Error = true, Message = e.Message });
            }

            return Json(new { Error = false, Message = "" });

        }

        public JsonResult Search(string term, int genusId)
        {
            ViewBag.GenusId = genusId;
            term = term.TrimAndRemoveDoubleSpaces();
            if (string.IsNullOrEmpty(term) || term.Length < 1)
            {
                return Json(new Object[] { }, JsonRequestBehavior.AllowGet);
            }
            int recordsToReturn = Properties.Settings.Default.RecordsToReturn;
            IEnumerable<DataListViewModel> genotypeList = map_repo.SearchAccessions(term, genusId, recordsToReturn);
            return Json(genotypeList, JsonRequestBehavior.AllowGet);
        }

    }
}