using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Util;
using BSGUtils;
using Usda.PlantBreeding.Core.Models;

namespace Usda.PlantBreeding.Site.Areas.Admin.Controllers
{
    [ActionFilters.AuthActionFilters]
    public class OriginsController : Controller
    {
        private IPlantBreedingRepo m_repo;

        public OriginsController() : this(new PlantBreedingRepo()) { }

        public OriginsController(IPlantBreedingRepo repo) 
        {
            m_repo = repo;
        }
        
        // GET: Origins
        public ActionResult Index(int? id)
        {
            //Check for origin id to scroll to and highlight
            if (id != null)
            {
                ViewBag.OriginId = id;
            }
            var origins = m_repo.GetOrigins();
            if (origins.IsNullOrEmpty())
           {
               return RedirectToAction("Create");
            }
            return View(origins);
        }

        // GET: Origins/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Origin origin = m_repo.GetOrigin(id.Value);
            
            if (origin == null)
            {
                return HttpNotFound();
            }

            return View(origin);
        }

        // GET: Origins/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Origins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Origin origin)
        {
            if (ModelState.IsValid)
            {
                m_repo.SaveOrigin(origin);
                return RedirectToAction("Index", new { id = origin.Id });
            }

            return View(origin);
        }

        // GET: Origins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Origin origin = m_repo.GetOrigin(id.Value);
            if (origin == null)
            {
                return HttpNotFound();
            }

            return View(origin);
        }

        // POST: Origins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Value")] Origin origin)
        {
            if (ModelState.IsValid)
            {
                m_repo.SaveOrigin(origin);
                return RedirectToAction("Index", new { id = origin.Id });
            }
            return View(origin);
        }

        public JsonResult Search(string term)
        {
            term = term.TrimAndRemoveDoubleSpaces();
            if (string.IsNullOrEmpty(term) || term.Length < 1)
            {
                return Json(new Object[] { }, JsonRequestBehavior.AllowGet);
            }
            int recordsToReturn = Properties.Settings.Default.RecordsToReturn;
            IEnumerable<DataListViewModel> originList = m_repo
                .GetOrigins(term, recordsToReturn)
                .Select(o => new DataListViewModel {
                    key = o.Id.ToString(),
                    value = o.Name
                });

            return Json(originList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Retire(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Origin origin = m_repo.GetOrigin(id.Value);
            if (origin == null)
            {
                return HttpNotFound();
            }
            origin.Retired = !origin.Retired;
            m_repo.SaveOrigin(origin);
            return RedirectToAction("Index", new { id = origin.Id });
        }
    }
}
