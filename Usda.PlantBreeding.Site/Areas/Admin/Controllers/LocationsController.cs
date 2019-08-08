using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;
using BSGUtils;
using System.Net;
using Usda.PlantBreeding.Core.Models;

namespace Usda.PlantBreeding.Site.Areas.Admin.Controllers
{
    public class LocationsController : Controller
    {

        private IPlantBreedingRepo m_repo;

        public LocationsController() : this(new PlantBreedingRepo()) { }

        public LocationsController(IPlantBreedingRepo repo)
        {
            m_repo = repo;
        }

        // GET: Locations
        public ActionResult Index(bool mapFlag = true)
        {
            var locations = m_repo.GetLocations();
            ViewBag.MapFlag = mapFlag;
            if (
                locations.IsNullOrEmpty())
            {
                return RedirectToAction("Create");
            }
            else
            {
                locations = locations.Where(x => x.MapFlag == mapFlag);
            }
            return View(locations);
        }

        // GET: Locations/Create
        public ActionResult Create(bool mapFlag = true)
        {
            var location = new Location { MapFlag = mapFlag };
            var locationVM = LocationViewModel.Create(location);
            return View("Edit", locationVM);
        }


        // GET: Locations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var location = m_repo.GetLocation(id.Value);
            var locationVM = LocationViewModel.Create(location);
            return View(locationVM);
        }

        // POST: Locations/Edit/5
        [HttpPost]
        public ActionResult Edit(LocationViewModel locationVM)
        {
            if (locationVM == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var location = LocationViewModel.ToLocation(locationVM);
            m_repo.SaveLocation(location);
            return Json(locationVM, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Search(string term, bool mapFlag = true)
        {
            term = term.TrimAndRemoveDoubleSpaces();
            if (string.IsNullOrEmpty(term) || term.Length < 1)
            {
                return Json(new Object[] { }, JsonRequestBehavior.AllowGet);
            }
            int recordsToReturn = Properties.Settings.Default.RecordsToReturn;
            var locationList = m_repo.GetLocations(term, recordsToReturn, mapFlag).Select(l => new { value = l.Id, label = l.Name });
            return Json(locationList, JsonRequestBehavior.AllowGet);
        }


        public JsonResult SearchLocations(string textVal, bool mapFlag = true)
        {
            textVal = textVal.TrimAndRemoveDoubleSpaces();
            if (string.IsNullOrEmpty(textVal) || textVal.Length < 1)
            {
                return Json(new Object[] { }, JsonRequestBehavior.AllowGet);
            }
            int recordsToReturn = Properties.Settings.Default.RecordsToReturn;

            var locationList = m_repo.GetLocations(textVal, recordsToReturn, mapFlag).Select(l => new DataListViewModel
            {
                key = l.Id.ToString(),
                value = l.Name
            });
            return Json(locationList, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Retire(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = m_repo.GetLocation(id.Value);
            if (location == null)
            {
                return HttpNotFound();
            }
            location.Retired = !location.Retired;
            m_repo.SaveLocation(location);
            return RedirectToAction("Index", new { mapFlag = location.MapFlag });
        }
    }
}
