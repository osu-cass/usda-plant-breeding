using System.Net;
using System.Web.Mvc;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Data.Util;
using BSGUtils;
namespace Usda.PlantBreeding.Site.Areas.Admin.Controllers
{
    [ActionFilters.AuthActionFilters]
    public class PloidiesController : Controller
    {

        private IPlantBreedingRepo m_repo;

        public PloidiesController() : this(new PlantBreedingRepo()) { }

        public PloidiesController(IPlantBreedingRepo repo)
        {
            m_repo = repo;
        }

        // GET: Ploidies
        public ActionResult Index()
        {
            var ploidies = m_repo.GetPloidies();
            if (ploidies.IsNullOrEmpty())
            {
                return RedirectToAction("Create");
            }
            return View(ploidies);
        }

        // GET: Ploidies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ploidy ploidy = m_repo.GetPloidyById(id.Value);
            if (ploidy == null)
            {
                return HttpNotFound();
            }
            return View(ploidy);
        }

        // GET: Ploidies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ploidies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ploidy ploidy)
        {
            if (ModelState.IsValid)
            {
                m_repo.SavePloidy(ploidy);
                return RedirectToAction("Index");
            }

            return View(ploidy);
        }

        // GET: Ploidies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ploidy ploidy = m_repo.GetPloidyById(id.Value);
            if (ploidy == null)
            {
                return HttpNotFound();
            }
            return View(ploidy);
        }

        // POST: Ploidies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ploidy ploidy)
        {
            if (ModelState.IsValid)
            {
                m_repo.SavePloidy(ploidy);
                return RedirectToAction("Index");
            }
            return View(ploidy);
        }

        public ActionResult Retire(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ploidy ploidy = m_repo.GetPloidyById(id.Value);
            if (ploidy == null)
            {
                return HttpNotFound();
            }
            ploidy.Retired = !ploidy.Retired;
            m_repo.SavePloidy(ploidy);
            return RedirectToAction("Index");
        }
    }
}
