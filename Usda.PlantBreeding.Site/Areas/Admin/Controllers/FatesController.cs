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
using BSGUtils;

namespace Usda.PlantBreeding.Site.Areas.Admin.Controllers
{
    public class FatesController : Controller
    {
        private IPlantBreedingRepo m_repo;

        public FatesController() : this(new PlantBreedingRepo()) { }

        public FatesController(IPlantBreedingRepo repo)
        {
            m_repo = repo;
        }

        // GET: Fates
        public ActionResult Index()
        {
            IEnumerable<Fate> fates = m_repo.GetFates();

            if (fates.IsNullOrEmpty())
            {
                return RedirectToAction("Create");
            }

            return View(fates.ToList());
        }

        // GET: Fates/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Fate fate = m_repo.GetFate(id.Value);

            if (fate == null)
            {
                return HttpNotFound();
            }

            return View(fate);
        }

        // GET: Fates/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Fates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Fate fate)
        {
            if (ModelState.IsValid)
            {
                fate.Order = m_repo.GetFates().Max(t => t.Order.GetValueOrDefault()) + 1;
                fate.Retired = false;
                m_repo.SaveFate(fate);
                return RedirectToAction("Index");
            }

            return View(fate);
        }

        // GET: Fates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Fate fate = m_repo.GetFate(id.Value);
            if (fate == null)
            {
                return HttpNotFound();
            }
            return View(fate);
        }

     
        // POST: Fates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Fate fate)
        {
            if (fate == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                m_repo.SaveFate(fate);
                return RedirectToAction("Index");
            }

            return View(fate);
        }

        public ActionResult Retire(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fate fate = m_repo.GetFate(id.Value);
            if (fate == null)
            {
                return HttpNotFound();
            }
            fate.Retired = !fate.Retired;
            m_repo.SaveFate(fate);
            return RedirectToAction("Index");
        }
    }
}
