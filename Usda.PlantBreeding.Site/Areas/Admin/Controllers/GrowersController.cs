using BSGUtils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Usda.PlantBreeding.Core.Models;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;

namespace Usda.PlantBreeding.Site.Areas.Admin.Controllers
{
    public class GrowersController : Controller
    {
        private IPlantBreedingRepo m_repo;

        /// <summary>
        /// Default constructor called by the system.
        /// </summary>
        public GrowersController() : this(new PlantBreedingRepo()) { }

        /// <summary>
        /// Overloaded constructor to facilitate dependency injection.
        /// </summary>
        public GrowersController(IPlantBreedingRepo repo)
        {
            m_repo = repo;
        }

        // GET: Admin/Growers
        public ActionResult Index()
        {
            return View(m_repo.GetGrowers().ToList());
        }

        // GET: Admin/Growers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Growers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Grower grower)
        {
            if (ModelState.IsValid)
            {
                m_repo.SaveGrower(grower);
                return RedirectToAction("Index");
            }

            return View(grower);
        }

        // GET: Admin/Growers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grower grower = m_repo.GetGrowers().SingleOrDefault(g => g.Id == id);
            if (grower == null)
            {
                return HttpNotFound();
            }
            return View(grower);
        }

        // POST: Admin/Growers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Grower grower)
        {
            if (ModelState.IsValid)
            {
                m_repo.SaveGrower(grower);
                return RedirectToAction("Index");
            }
            return View(grower);
        }

        /// <summary>
        /// Search object, list of locations defined by records to return and search term
        /// </summary>
        public JsonResult SearchGrowers(string textVal)
        {
            textVal = textVal.TrimAndRemoveDoubleSpaces();
            if (string.IsNullOrEmpty(textVal) || textVal.Length < 1)
            {
                return Json(new Object[] { }, JsonRequestBehavior.AllowGet);
            }
            int recordsToReturn = Properties.Settings.Default.RecordsToReturn;


            var growers = m_repo.GetGrowers(textVal, recordsToReturn).Select(g => new DataListViewModel
            {
                key = g.Id.ToString(),
                value = $"{g.FirstName} {g.LastName}"
            });


            return Json(growers, JsonRequestBehavior.AllowGet);
        }

    }
}
