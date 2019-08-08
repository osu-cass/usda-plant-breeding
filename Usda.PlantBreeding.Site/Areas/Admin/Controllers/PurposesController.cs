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
    public class PurposesController : Controller
    {
        private IPlantBreedingRepo m_repo;

        public PurposesController() : this(new PlantBreedingRepo()) { }

        public PurposesController(IPlantBreedingRepo repo)
        {
            m_repo = repo;
        }

        // GET: Purposes
        public ActionResult Index()
        {
            var Purposes = m_repo.GetPurposes();
            if (Purposes.IsNullOrEmpty())
            {
                return RedirectToAction("Create");
            }
            return View(Purposes);
        }

        // GET: Purposes/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Purpose Purpose = m_repo.GetPurpose(id.Value);

            if (Purpose == null)
            {
                return HttpNotFound();
            }

            return View(Purpose);
        }

        // GET: Purposes/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Purposes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Purpose Purpose)
        {
            if (ModelState.IsValid)
            {
                m_repo.SavePurpose(Purpose);
                return RedirectToAction("Index");
            }

            return View(Purpose);
        }

        // GET: Purposes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purpose Purpose = m_repo.GetPurpose(id.Value);
            if (Purpose == null)
            {
                return HttpNotFound();
            }
            return View(Purpose);
        }

        // POST: Purposes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Purpose Purpose)
        {
            if (ModelState.IsValid)
            {
                m_repo.SavePurpose(Purpose);
                return RedirectToAction("Index");
            }
            return View(Purpose);
        }


        public ActionResult Retire(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purpose Purpose = m_repo.GetPurpose(id.Value);
            if (Purpose == null)
            {
                return HttpNotFound();
            }
            Purpose.Retired = !Purpose.Retired;
            m_repo.SavePurpose(Purpose);
            return RedirectToAction("Index");
        }
    }
}
