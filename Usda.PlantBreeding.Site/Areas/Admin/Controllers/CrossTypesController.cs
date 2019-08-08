using System.Linq;
using System.Net;
using System.Web.Mvc;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Data.Util;
using BSGUtils;

namespace Usda.PlantBreeding.Site.Areas.Admin.Controllers
{
    [ActionFilters.AuthActionFilters]
    public class CrossTypesController : Controller
    {
        private IPlantBreedingRepo m_repo;

        /// <summary>
        /// Default constructor called by the system.
        /// </summary>
        public CrossTypesController() : this(new PlantBreedingRepo()) { }

        /// <summary>
        /// Overloaded constructor to facilitate dependency injection.
        /// </summary>
        public CrossTypesController(IPlantBreedingRepo repo)
        {
            m_repo = repo;
        }

        // GET: CrossTypes
        public ActionResult Index()
        {
            var genera = m_repo.GetAllGenera();
            if (!genera.IsNullOrEmpty())
            {
                return View(genera);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult IndexByGenus(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }

            var crossTypes = m_repo.GetCrossTypes().Where(c => c.GenusId == id);
            if (crossTypes.IsNullOrEmpty())
            {
                return RedirectToAction("Create");
            }
            return View(crossTypes.ToList());
        }

        // GET: CrossTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue) 
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CrossType crossType = m_repo.GetCrossType(id.Value);

            if (crossType == null)
            {
                return HttpNotFound();
            }

            return View(crossType);
        }

        // GET: CrossTypes/Create
        public ActionResult Create()
        {
            ViewBag.genusId = new SelectList(m_repo.GetAllGenera(), "Id", "Value");
            return View();
        }

        // POST: CrossTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Value,GenusId")] CrossType crossType)
        {
            if (ModelState.IsValid)
            {
                m_repo.SaveCrossType(crossType);

                return RedirectToAction("IndexByGenus", new { id = crossType.GenusId });
            }

            return View(crossType);
        }

        // GET: CrossTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CrossType crossType = m_repo.GetCrossType(id.Value);

            if (crossType == null)
            {
                return HttpNotFound();
            }

            return View(crossType);
        }

        // POST: CrossTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Value,GenusId")] CrossType crossType)
        {
            if (ModelState.IsValid)
            {
                m_repo.SaveCrossType(crossType);

                return RedirectToAction("IndexByGenus", new { id = crossType.GenusId });
            }

            return View(crossType);
        }

        public ActionResult Retire(int? id, int? genus)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CrossType crossType = m_repo.GetCrossType(id.Value);
            if (crossType == null)
            {
                return HttpNotFound();
            }

            crossType.Retired = !crossType.Retired;
            m_repo.SaveCrossType(crossType);
            return RedirectToAction("IndexByGenus", new { id = genus });
        }
    }
}
