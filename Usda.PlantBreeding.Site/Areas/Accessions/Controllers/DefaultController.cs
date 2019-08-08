using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;
using BSGUtils;
using PagedList;
using Usda.PlantBreeding.Core.Interfaces;
using Usda.PlantBreeding.Core.Infrastructure;
using Usda.PlantBreeding.Core.Models;

namespace Usda.PlantBreeding.Site.Areas.Accessions.Controllers
{
    [ActionFilters.AuthActionFilters]
    public class DefaultController : Controller
    {
        private IPlantBreedingRepo m_repo;
        private IAccessionUOW a_repo;

        public DefaultController() : this(new PlantBreedingRepo()) { }

        public DefaultController(IPlantBreedingRepo repo)
        {
            m_repo = repo;
            a_repo = new AccessionUOW(repo);
        }

        public ActionResult Test()
        {
            return View();
        }
        /// <summary>
        /// Autocomplete search for genotypes with original name (or name) containing given term.
        /// </summary>
        /// <param name="term">
        /// Term to search for in original name (or name) of genotypes.
        /// </param>
        /// <returns>
        /// Returns JSON array of genotypes(name, original name, id) whose original name (or name) contains given term.
        /// </returns>
        public JsonResult Search(string term, int genusId)
        {
            ViewBag.GenusId = genusId;
            term = term.TrimAndRemoveDoubleSpaces();
            if (string.IsNullOrEmpty(term) || term.Length < 1)
            {
                return Json(new Object[] { }, JsonRequestBehavior.AllowGet);
            }
            int recordsToReturn = Properties.Settings.Default.RecordsToReturn;
            IEnumerable<DataListViewModel> genotypeList = a_repo.Search(term, genusId, recordsToReturn);
            return Json(genotypeList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Autocomplete search for genotypes with original name (or name) containing given term.
        /// </summary>
        /// <param name="textVal">
        /// Term to search for in original name (or name) of genotypes.
        /// </param>
        /// <returns>
        /// Returns JSON array of genotypes(name, original name, id) whose original name (or name) contains given term.
        /// </returns>
        public JsonResult SearchAccessions(string textVal, int genusId)
        {
            ViewBag.GenusId = genusId;
            textVal = textVal.TrimAndRemoveDoubleSpaces();
            if (string.IsNullOrEmpty(textVal) || textVal.Length < 1)
            {
                return Json(new Object[] { }, JsonRequestBehavior.AllowGet);
            }
            int recordsToReturn = Properties.Settings.Default.RecordsToReturn;
            IEnumerable<DataListViewModel> genotypeList = a_repo.Search(textVal, genusId, recordsToReturn);
            return Json(genotypeList, JsonRequestBehavior.AllowGet);
        }


        private void SetPageSizeViewBag()
        {
            IEnumerable<SelectListItem> recordsPage = new List<SelectListItem>()
            {
                new SelectListItem() {Text="20", Value="20"},
                new SelectListItem() {Text="50", Value="50"},
                new SelectListItem() {Text="100", Value="100"},
                new SelectListItem() {Text="250", Value="250"},
                new SelectListItem() {Text="500", Value="500"},
                new SelectListItem() {Text="1000", Value="1000"},
                new SelectListItem() {Text="All", Value="0"}
            };

            ViewBag.PageSizeList = recordsPage;
        }


        [HttpGet]
        [ActionFilters.GenusActionFilter]
        public ActionResult Index(int? pageSize, int? genotypeId, string filter, int page = 1, string messageResult = "")
        {
            int defaultPage = 1;
            int baseIdResult = 0;
            int genotypeIdResult = 0;
            bool successfullResult = true;
            string filterResult = filter;

            if (!pageSize.HasValue)
            {
                if (SessionManager.SessionPageSize.HasValue)
                {
                    pageSize = SessionManager.SessionPageSize;
                }
                else
                {
                    pageSize = Properties.Settings.Default.PageSize;
                }
            }
            SetPageSizeViewBag();
            SessionManager.SessionPageSize = pageSize.Value;
            int genusId = SessionManager.GetGenusId().Value;

            IPagedList<AccessionIndexViewModel> pageOfAccessions = null;
            try
            {
                pageOfAccessions = a_repo.GetPageInIndex(pageSize.Value, genotypeId, filter, page, genusId);

            }
            catch (SearchException e)
            {
                successfullResult = false;
                messageResult = e.Message;
            }

            if (successfullResult == false)
            {
                try
                {
                    pageOfAccessions = a_repo.GetPageInIndex(pageSize.Value, defaultPage, genusId);

                }
                catch (SearchException)
                {
                    return RedirectToAction("Create");
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }

            Genotype genotypeSearched = null;
            if (!genotypeId.HasValue && !filter.IsNullOrWhiteSpace())
            {
                genotypeSearched = m_repo.GetGenotypes(filter, genusId, 1).FirstOrDefault();
            }
            else if (genotypeId.HasValue)
            {
                genotypeSearched = m_repo.GetGenotype(genotypeId.Value);
            }

            if (genotypeSearched != null)
            {
                successfullResult = true;
                genotypeIdResult = genotypeSearched.Id;
                if (genotypeSearched.Family.BaseGenotypeId.HasValue && pageOfAccessions.HasAny(t => t.Id == genotypeSearched.Family.BaseGenotypeId))
                {
                    baseIdResult = genotypeSearched.Family.BaseGenotypeId.Value;
                }
                else
                {
                    baseIdResult = genotypeSearched.Id;
                }
            }

            ViewBag.GenusId = genusId;
            ViewBag.GenotypeId = genotypeIdResult;
            ViewBag.BaseId = baseIdResult;
            ViewBag.SuccessfulSearch = successfullResult;
            ViewBag.Filter = filter;
            ViewBag.SearchErrorMessage = messageResult;
            ViewBag.PageSize = pageSize.Value.ToString();
            ViewBag.CurrentDate = DateTime.Now;

            return View(pageOfAccessions);
        }


        [ActionFilters.GenusActionFilter]
        public ActionResult Create()
        {
            AccessionViewModel accession = a_repo.GetNextAccession(SessionManager.GetGenusId().Value);
            SetViewBagForPlant(accession);
            ViewBag.Create = true;
            return View(accession);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccessionViewModel accession)
        {
            if (accession == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                a_repo.SaveAccessionViewModel(accession);
                return RedirectToAction("Details", "Default", new { id = accession.Id });
            }
            ViewBag.Create = true;
            SetViewBagForPlant(accession);
            return View(accession);
        }


        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessionViewModel accession = a_repo.GetAccessionViewModel(id.Value);
            if (accession == null)
            {
                return HttpNotFound();
            }
            SetViewBagForPlant(accession);
            ViewBag.Create = false;
            return View(accession);
        }

        // TODO: save for types?
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(AccessionViewModel accession)
        {
            if (accession == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                a_repo.SaveAccessionViewModel(accession);
                return RedirectToAction("Details", "Default", new { id = accession.Id });
            }
            SetViewBagForPlant(accession);
            a_repo.RestoreProperties(accession);
            return View(accession);
        }

        public ActionResult DetailsModal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genotype genotype = m_repo.GetGenotype(id.Value);
            if (genotype == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DetailsModal", genotype);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DetailsModal(int id)
        {
            var year = DateTime.Now.Year;
            Candidate candidate = m_repo.GetCandidate(id, year);
            if (candidate == null)
            {
                candidate = new Candidate
                {
                    GenotypeId = id,
                    Year = year
                };
                m_repo.SaveCandidate(candidate);
            }
            else
            {
                m_repo.DeleteCandidate(candidate);
            }
            Genotype genotype = m_repo.GetGenotype(id);
            if (genotype == null)
            {
                return RedirectToAction("List");
            }
            return PartialView("_DetailsModal", genotype);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genotype genotype = m_repo.GetGenotype(id.Value);
            if (genotype == null)
            {
                return HttpNotFound();
            }
            return View(genotype);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Genotype genotype = m_repo.GetGenotype(id);
            m_repo.DeleteGenotype(genotype);
            return RedirectToAction("Index");
        }

        [Obsolete]
        [ActionFilters.GenusActionFilter]
        private void SetViewBagForPlant(Genotype genotype)
        {
            if (genotype != null)
            {
                IEnumerable<Family> families = m_repo.GetFamilies(f => f.Genus.Id == SessionManager.GetGenusId().Value);
                ViewBag.FamilyId = new SelectList(families, "Id", "Name", genotype.FamilyId);
                IEnumerable<SelectListItem> ploidies = m_repo
                    .GetPloidies()
                    .Select(p => new SelectListItem()
                    {
                        Value = p.Id.ToString(),
                        Text = p.Name,
                        Disabled = p.Retired
                    });
                ViewBag.PloidyId = ploidies;
            }
        }

        [ActionFilters.GenusActionFilter]
        private void SetViewBagForPlant(AccessionViewModel accession)
        {
            // TODO: use accession unit of work methods

            IEnumerable<SelectListItem> crossTypes = m_repo
                .GetCrossTypes()
                .Where(c => !c.Retired && c.GenusId == accession.FamilyGenusId || c.Id == accession.FamilyCrossTypeId)
                .Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                    Disabled = c.Retired
                }).OrderBy(t => t.Text);
            ViewBag.CrossTypeId = crossTypes;
        }

        [ActionFilters.GenusActionFilter]
        public ActionResult CreateModal(string searchInput)
        {
            IEnumerable<Family> families = m_repo.GetFamilies(f => f.Genus.Id == SessionManager.GetGenusId().Value);
            ViewBag.FamilyId = new SelectList(families, "Id", "Name");
            return PartialView("_CreateModal", searchInput);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateModal(Genotype plant, string searchInput)
        {
            if (plant == null)
            {
                return PartialView("_CreateModal");
            }
            if (ModelState.IsValid)
            {
                m_repo.SaveGenotype(plant);
                return RedirectToAction("Details", "Families", new { id = plant.FamilyId });
            }
            SetViewBagForPlant(plant);
            return PartialView("_CreateModal", plant);
        }


        [HttpPost]
        public JsonResult CreateSelection(int? id)
        {
            if (!id.HasValue)
            {
                return Json(new { Error = true, Message = "Bad Request" });
            }
            Genotype genotype = a_repo.CreateGenotypeSelection(id.Value);

            return Json(new { Error = false, Message = "Success " + genotype.Name });

        }


        [HttpGet]
        public ActionResult DeleteSelection(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var genotype = a_repo.GetGenotype(id.Value);

            if(genotype == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string message = $"Success, deleted record {genotype.Name}";

            a_repo.DeleteSelection(id.Value);

            return RedirectToAction(nameof(Index), new { messageResult = message });

        }

        [HttpGet]
        public ActionResult CreatePopulationFromSelection(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                a_repo.CreatePopulationFromGenotype(id.Value);

            }
            catch (ArgumentNullException)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return RedirectToAction(nameof(Details), new { id = id.Value });

        }
        [HttpPost]
        public ActionResult UpdateIsPopulation(AccessionIndexViewModel aivm)
        {
            bool error = false;
            string message = string.Empty;
            try
            {
                a_repo.UpdateGenotypeIsPopulation(aivm.Id, aivm.IsPopulation);

            }
            catch (AccessionException e)
            {
                error = true;
                message = e.Message;
            }

            return Json(new { Error = error, Message = message });
        }
    }
}
