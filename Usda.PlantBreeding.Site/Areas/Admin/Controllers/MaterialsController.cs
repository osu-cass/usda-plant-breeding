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
    public class MaterialsController : Controller
    {
        private IPlantBreedingRepo m_repo;

        /// <summary>
        /// Default constructor called by the system.
        /// </summary>
        public MaterialsController() : this(new PlantBreedingRepo()) { }

        /// <summary>
        /// Overloaded constructor to facilitate dependency injection.
        /// </summary>
        public MaterialsController(IPlantBreedingRepo repo)
        {
            m_repo = repo;
        }

        // GET: Admin/Materials
        public ActionResult Index()
        {
            return View(m_repo.GetMaterials().ToList());
        }

        // GET: Admin/Materials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material material = m_repo.GetMaterials().SingleOrDefault(m => m.Id == id);
            if (material == null)
            {
                return HttpNotFound();
            }
            return View(material);
        }

        // GET: Admin/Materials/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Materials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Material material)
        {
            if (ModelState.IsValid)
            {
                m_repo.SaveMaterial(material);
                return RedirectToAction("Index");
            }

            return View(material);
        }

        // GET: Admin/Materials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material material = m_repo.GetMaterials().SingleOrDefault(m => m.Id == id);
            if (material == null)
            {
                return HttpNotFound();
            }
            return View(material);
        }

        // POST: Admin/Materials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Material material)
        {
            if (ModelState.IsValid)
            {
                m_repo.SaveMaterial(material);
                return RedirectToAction("Index");
            }
            return View(material);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material material = m_repo.GetMaterial(id.Value);
            if (material == null)
            {
                return HttpNotFound();
            }
            m_repo.DeleteMaterial(material);
            return RedirectToAction("Index");
        }
    }
}
