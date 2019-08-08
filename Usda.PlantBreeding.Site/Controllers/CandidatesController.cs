using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Data.Util;
using BSGUtils;
using System.Collections.Generic;

namespace Usda.PlantBreeding.Site.Controllers
{
    [ActionFilters.AuthActionFilters]
    public class CandidatesController : Controller
    {
        private IPlantBreedingRepo m_repo;

        public CandidatesController() : this(new PlantBreedingRepo()) { }

        public CandidatesController(IPlantBreedingRepo repo)
        {
            m_repo = repo;
        }

        // GET: Candidates
        [ActionFilters.GenusActionFilter]
        public ActionResult Index()
        {
            IEnumerable<Candidate> candidates = m_repo.GetCandidates(c => c.Genotype.Family.GenusId ==SessionManager.GetGenusId().Value);
            if (candidates.IsNullOrEmpty())
            {
                return RedirectToAction("List", "Default");
            }

            return View(candidates);
        }

        //GET: Candidates/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Candidate candidate = m_repo.GetCandidate(id.Value);
            if (candidate == null)
            {
                return HttpNotFound();
            }
          
            return View(candidate);
        }

        //GET: Candidates/Details/5
        public ActionResult CandidatesForCross()
        {
            IEnumerable<Candidate> candidates = m_repo.GetCandidates();
            if (candidates.IsNullOrEmpty())
            {
                return HttpNotFound();
            }

            IEnumerable<Candidate> matches = candidates.Where(c => c.Year == DateTime.Now.Year);
            if (matches.IsNullOrEmpty())
            {
                return HttpNotFound();
            }

            return View(candidates);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CandidatesForCross(int Candidate1, int Candidate2)
        {
            return RedirectToAction("CreateFromCandidate", "Families", new { candidate1 = Candidate1, candidate2 = Candidate2 });
        }

        // GET: Candidates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Candidate candidate = m_repo.GetCandidate(id.Value);
            if (candidate == null)
            {
                return HttpNotFound();
            }

            return View(candidate);
        }

        // POST: Candidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Candidate candidate = m_repo.GetCandidate(id);
            m_repo.DeleteCandidate(candidate);

            return RedirectToAction("Index");
        }
    }
}
