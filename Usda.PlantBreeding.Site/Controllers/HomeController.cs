using System.Web;
using System.Web.Mvc;
using Usda.PlantBreeding.Data.DataAccess;
using BSGUtils;
using Usda.PlantBreeding.Data.Models;
using System.Net;
using System.Linq;
namespace Usda.PlantBreeding.Site.Controllers
{
    [ActionFilters.AuthActionFilters]
    public class HomeController : Controller
    {
        private IPlantBreedingRepo m_repo;

        public HomeController() : this(new PlantBreedingRepo()) { }

        public HomeController(IPlantBreedingRepo repo)
        {
            m_repo = repo;
        }

        public ActionResult Index(bool? firstLoad, int? genusId)
        {
            var user = User.Identity;
            ViewBag.IsAuth = user.IsAuthenticated;
            ViewBag.Name = user.Name;
            var sessionGenusId = SessionManager.GetGenusId();
           
            if (firstLoad.HasValue && firstLoad.Value == false && !genusId.HasValue)
            {
                ViewBag.firstLoad = "false";
            }
            if (genusId.HasValue && genusId.Value != sessionGenusId)
            {
                Genus genus = m_repo.GetGenus(genusId.Value);

                if (genus != null)
                {
                    SessionManager.Genus = genus;
                    sessionGenusId = genus.Id;
                }
            }
            ViewBag.genusId = new SelectList(m_repo.GetAllGenera(), "Id", "Value", sessionGenusId);
            ViewBag.SelectedGenusId = sessionGenusId.GetValueOrDefault();
            return View();
        }

        public ActionResult GeneraImage(string name)
        {

            ActionResult result = null;
            Genus genus = m_repo.GetAllGenera().SingleOrDefault(t => t.Value.ToLower().Contains(name.ToLower()));

            if (genus == null)
            {
                result = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                result = RedirectToAction("Index", new { genusId = genus.Id });
            }

            return result;
        }

        [HttpPost]
        //Todo: fix redirect if null
        public ActionResult Index(int? genusId)
        {
            ActionResult result = null;

            if (!genusId.HasValue)
            {
                return RedirectToAction("Index");
            }


            Genus genus = m_repo.GetGenus(genusId.Value);

            if (genus == null)
            {
                result = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                SessionManager.Genus = genus;
                string url = SessionManager.RedirectUrl;

                if (!url.IsNullOrWhiteSpace())
                {
                    result = Redirect(url);
                    SessionManager.RedirectUrl = string.Empty;
                }
                else
                {
                    result = RedirectToAction("Index", "Default", new { Area = "Accessions" });

                }
            }

            return result;
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

     
    }
}