using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;

namespace Usda.PlantBreeding.Site.Areas.Admin.Controllers
{
    public class FlatTypesController : Controller
    {
        private IPlantBreedingRepo m_repo;

        public FlatTypesController() : this(new PlantBreedingRepo()) { }

        public FlatTypesController(PlantBreedingRepo repo)
        {
            m_repo = repo;
        }

        // GET: Admin/FlatTypes
        public ActionResult Index()
        {
            return View(m_repo.GetFlatTypes());
        }

        // GET: Admin/FlatTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlatType flatType = m_repo.GetFlatType(id.Value);
            if (flatType == null)
            {
                return HttpNotFound();
            }
            return View(flatType);
        }

        // GET: Admin/FlatTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/FlatTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Value")] FlatType flatType)
        {
            if (ModelState.IsValid)
            {
                m_repo.SaveFlatType(flatType);
                return RedirectToAction("Index");
            }
            return View(flatType);
        }

        // GET: Admin/FlatTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlatType flatType = m_repo.GetFlatType(id.Value);
            if (flatType == null)
            {
                return HttpNotFound();
            }
            return View(flatType);
        }

        // POST: Admin/FlatTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Value")] FlatType flatType)
        {
            if (flatType == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                m_repo.SaveFlatType(flatType);
                return RedirectToAction("Index");
            }
            return View(flatType);
        }

        public ActionResult Retire(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlatType flatType = m_repo.GetFlatType(id.Value);
            if (flatType == null)
            {
                return HttpNotFound();
            }
            flatType.Retired = !flatType.Retired;
            m_repo.SaveFlatType(flatType);
            return RedirectToAction("Index");
        }
    }
}
