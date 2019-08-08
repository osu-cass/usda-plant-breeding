using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Usda.Lib.DataAccess;
using Usda.Lib.Models;
using Usda.Lib.Util;
using PagedList;
using Microsoft.Reporting.WebForms;
using BSGUtils;
using System.Configuration;

namespace Usda.SmallFruits.Areas.Accessions.Controllers
{
    [ActionFilters.AuthActionFilters]
    public class FamiliesController : Controller
    {
        private IUSDAFruitsRepo m_repo;

        public FamiliesController() : this(new USDAFruitsRepo()) { }

        public FamiliesController(IUSDAFruitsRepo repo)
        {
            m_repo = repo;
        }

        /// <summary>
        /// Autocomplete search for families.
        /// </summary>
        /// <param name="term">
        /// Term to search for in original name (or name) of genotypes.
        /// </param>
        /// <returns>
        /// Returns JSON array of family(name, original name, id) whose original name (or name) contains given term.
        /// </returns>
        /// 
        public JsonResult Search(string term, int genusId)
        {
            ViewBag.GenusId = genusId;
            term = term.TrimAndRemoveDoubleSpaces();
            if (string.IsNullOrEmpty(term) || term.Length < 1)
            {
                return Json(new Object[] { }, JsonRequestBehavior.AllowGet);
            }
            int recordsToReturn = Properties.Settings.Default.RecordsToReturn;
            IEnumerable<Family> families = m_repo.GetFamilies(term, genusId, recordsToReturn);
            //combine the names and original names of the list
            var familyGivenNames = families
                .Where(t => !t.GivenName.IsNullOrWhiteSpace())
                .Select(t => new { value = t.Id, label = t.GivenName });
            var familiesListOriginal = families.Select(t => new { value = t.Id, label = t.OriginalName });
            var familiesList = familyGivenNames.Union(familiesListOriginal).ToArray();
            return Json(familiesList, JsonRequestBehavior.AllowGet);
        }

        private void SetPageSizeViewBag(int pagesize)
        {
            IEnumerable<SelectListItem> recordsPage = new List<SelectListItem>()
            {
                new SelectListItem() {Text="10", Value="10"},
                new SelectListItem() {Text="20", Value="20"},
                new SelectListItem() {Text="50", Value="50"},
                new SelectListItem() {Text="All", Value="0"}
            };
            var item = recordsPage.FirstOrDefault(t => t.Value == pagesize.ToString());
            if (item != null)
            {
                item.Selected = true;
            }
            ViewBag.PageSize = recordsPage;
        }

        private int FindFamilyInPage(IQueryable<Family> families, int? id, int pageSize, int genusId, string searchterm = null)
        {
            int result = 1;
            Family family;
            if (id.HasValue)
            {
                family = m_repo.GetFamily(id.Value);
            }
            else
            {
                family = m_repo.GetFamilies(searchterm, genusId, 1).FirstOrDefault();

            }
            if (family != null)
            {
                int familyIndex = families.ToList().FindIndex(t => t.Id == family.Id);
                if (familyIndex > 0 && pageSize > 0)
                {
                    result = (int)Math.Ceiling((decimal)familyIndex / pageSize);
                }
            }
            return result;
        }

        // helper method used to check if page request is reasonable
        private bool isReasonableRequest(int page, int total, int pageSize)
        {
            // total records is divisable by page size
            bool p = total % pageSize == 0 && total / pageSize - page >= 0;
            // total records is not divisable by page records
            bool q = total % pageSize != 0 && total / pageSize + 1 - page >= 0;
            // is request is reasonable?
            return pageSize > 0 && page > 0 && (p || q);
        }

        [HttpGet]
        [ActionFilters.GenusActionFilter]
        public ActionResult Index(int? pageSize, int? familyId, string filter = "", int page = 1)
        {
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
            SetPageSizeViewBag(pageSize.Value);
            int genusId = SessionManager.GenusId.Value;
            IQueryable<Family> families = m_repo.GetQueryableFamilies(f => f.GenusId == genusId);
            if (families.IsNullOrEmpty())
            {
                return RedirectToAction("Create");
            }
            filter = filter.TrimAndRemoveDoubleSpaces();
            //get the genotype in page
            if (!filter.IsNullOrWhiteSpace() || familyId.HasValue)
            {
                page = FindFamilyInPage(families, familyId, pageSize.Value, genusId, filter);
            }
            // check if client request is reasonable
            int total = families.Count();
            // zero is all records
            pageSize = pageSize.Value == 0 ? total : pageSize.Value;
            if (isReasonableRequest(page, total, pageSize.Value))
            {
                // reasonable request
                IPagedList<Family> pageOfFamilies = families.ToPagedList(page, pageSize.Value);
                ViewBag.GenusId = genusId;
                return View(pageOfFamilies);
            }
            else
            {
                // unreasonable request
                return RedirectToAction("Index");
            }
        }

    
        // GET: Families/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Family family = m_repo.GetFamily(id.Value);
            ViewBag.NameCrumb = family.GivenName;
            if (family == null)
            {
                return HttpNotFound();
            }
            family.Genotypes = family.Genotypes.OrderBy(t => t.SelectionNum).ToList();
            return View(family);
        }

        // GET: Families/Create
        [ActionFilters.GenusActionFilter]
        public ActionResult Create()
        {
            Origin origin = m_repo.GetDefaultOrigin();
            int genusId = SessionManager.GenusId.Value;
            Family family = new Family()
            {
                Year = DateTime.Now.Year.ToString(),
                GenusId = SessionManager.GenusId.Value,
                Origin = origin,
                OriginId = origin.Id,
                CrossNum = m_repo.GetNextCross(origin.Id, genusId)
            };
            ViewBag.CrossTypeId = new SelectList(m_repo.GetCrossTypes(), "Id", "Name");
            ViewBag.OriginId = new SelectList(m_repo.GetOrigins(), "Id", "Name", family.Origin);
            ViewBag.PloidyId = new SelectList(m_repo.GetPloidies(), "Id", "Name");
            ViewBag.MaleParent = new SelectList(m_repo.GetGenotypes(), "Id", "Name");
            ViewBag.FemaleParent = new SelectList(m_repo.GetGenotypes(), "Id", "Name");
            return View(family);
        }

        // POST: Families/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Family family)
        {
            if (ModelState.IsValid)
            {
                m_repo.SaveFamily(family);
                Genotype genotype = new Genotype()
                {
                    FamilyId = family.Id,
                    Year = family.Year,
                };
                m_repo.SaveGenotype(genotype);
                return RedirectToAction("Details", new { id = family.Id });
            }
            SetViewBagForInvalidSession(family);
            return View(family);
        }

        [ActionFilters.GenusActionFilter]
        public ActionResult CreateFromCandidate(int candidate1, int candidate2)
        {
            Candidate candidatebyId1, candidatebyId2;
            candidatebyId1 = m_repo.GetCandidate(candidate1);
            candidatebyId2 = m_repo.GetCandidate(candidate2);
            Family family = new Family();
            family.MaleGenotype = candidatebyId1.Genotype;
            family.FemaleGenotype = candidatebyId2.Genotype;
            ViewBag.CrossTypeId = new SelectList(m_repo.GetCrossTypes(), "Id", "Name");
            ViewBag.GenusId = new SelectList(m_repo.GetAllGenera(), "Id", "Name", SessionManager.GenusId.Value);
            ViewBag.OriginId = new SelectList(m_repo.GetOrigins(), "Id", "Name");
            ViewBag.PloidyId = new SelectList(m_repo.GetPloidies(), "Id", "Name");
            ViewBag.MaleParent = new SelectList(m_repo.GetGenotypes(), "Id", "Name");
            ViewBag.FemaleParent = new SelectList(m_repo.GetGenotypes(), "Id", "Name");
            return View(family);
        }

        // POST: Families/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFromCandidate(Family family)
        {
            if (family == null)
            {
                return RedirectToAction("Create");
            }
            if (ModelState.IsValid)
            {
                m_repo.SaveFamily(family);
                return RedirectToAction("Details", new { id = family.Id });
            }
            SetViewBagForInvalidSession(family);
            return View(family);
        }

        // GET: Families/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Family family = m_repo.GetFamily(id.Value);
            if (family == null)
            {
                return HttpNotFound();
            }
            SetViewBagForInvalidSession(family);
            return View(family);
        }

        // POST: Families/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Family family)
        {
            if (family == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                m_repo.SaveFamily(family);
                return RedirectToAction("Details", new { id = family.Id });
            }
            SetViewBagForInvalidSession(family);
            return View(family);
        }

        // GET: Families/Edit/5
        public ActionResult IndexEdit(int? id, int? pageSize)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (pageSize.HasValue)
            {
                SessionManager.SessionPageSize = pageSize.Value;
            }
            Family family = m_repo.GetFamily(id.Value);
            if (family == null)
            {
                return HttpNotFound();
            }
            SetViewBagForInvalidSession(family);
            return View(family);
        }

        // POST: Families/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IndexEdit(Family family)
        {
            if (family == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                m_repo.SaveFamily(family);
                return RedirectToAction("Index", "Families", new { familyId = family.Id });
            }
            SetViewBagForInvalidSession(family);
            return View(family);
        }

        private void SetViewBagForInvalidSession(Family family)
        {
            if (family != null)
            {
                IEnumerable<SelectListItem> crossTypes = m_repo
                    .GetCrossTypes()
                    .Select(c => new SelectListItem()
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name,
                        Disabled = c.Retired
                    });
                IEnumerable<SelectListItem> ploidies = m_repo
                    .GetPloidies()
                    .Select(p => new SelectListItem()
                    {
                        Value = p.Id.ToString(),
                        Text = p.Name,
                        Disabled = p.Retired
                    });
                ViewBag.CrossTypeId = crossTypes;
                ViewBag.GenusId = new SelectList(m_repo.GetAllGenera(), "Id", "Name", family.GenusId);
                ViewBag.OriginId = new SelectList(m_repo.GetOrigins(), "Id", "Name", family.OriginId);
                ViewBag.PloidyId = ploidies;
                ViewBag.MaleParent = new SelectList(m_repo.GetGenotypes(), "Id", "Name", family.MaleGenotype);
                ViewBag.FemaleParent = new SelectList(m_repo.GetGenotypes(), "Id", "Name", family.FemaleGenotype);
            }
        }

        //TODO: fix this, SessionManager?
        public ActionResult DetailsReport(int familyId)
        {
            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("FamilyId", familyId.ToString()));
            TempData["reportParameters"] = parameters;
            TempData["reportPath"] = "Genotypes";
            return RedirectToAction("ReportViewer", "Reports");
        }

        //TODO: fix this, SessionManager?
        public ActionResult IndexReport(int genusId)
        {
            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("GenusId", genusId.ToString()));
            TempData["reportParameters"] = parameters;
            TempData["reportPath"] = "Families";
            return RedirectToAction("ReportViewer", "Reports");
        }
    }
}
