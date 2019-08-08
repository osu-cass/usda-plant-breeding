using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Usda.PlantBreeding.Data.Util;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Site.Models;
using Usda.PlantBreeding.Site.Models.Translators;
using System.Net;
using BSGUtils;
using Usda.PlantBreeding.Site.Models.Helpers;
using System;
using Usda.PlantBreeding.Core.Models;
using Usda.PlantBreeding.Core.Interfaces;
using Usda.PlantBreeding.Core.Infrastructure;

namespace Usda.PlantBreeding.Site.Areas.Accessions.Controllers
{
    [ActionFilters.AuthActionFilters]
    public class SelectionSummaryController : Controller
    {
        private IPlantBreedingRepo m_repo;
        private ISelectionSummaryUOW s_repo;

        public SelectionSummaryController() : this(new PlantBreedingRepo()) { }

        public SelectionSummaryController(IPlantBreedingRepo repo)
        {
            m_repo = repo;
            s_repo = new SelectionSummaryUOW(repo);

        }

        [HttpGet]
        [ActionFilters.GenusActionFilter]
        public ActionResult Index(string filter = "")
        {
            filter = filter.TrimAndRemoveDoubleSpaces();
            int genusId = SessionManager.GetGenusId().Value;
            ViewBag.GenusId = genusId;

            if (filter.IsNullOrWhiteSpace())
            {
                return View();
            }

            ViewBag.Searched = true;
            ViewBag.Filter = filter;


            int RecordsToReturn = Properties.Settings.Default.RecordsToReturn;

            IEnumerable<Genotype> genotypes = m_repo.GetQueryableGenotypes(filter.TrimAndRemoveDoubleSpaces(), genusId).Take(RecordsToReturn);
            genotypes.OrderBy(g => g.Family.Name);

            if (genotypes.Count() == 1)
            {
                return RedirectToAction("Detail", "SelectionSummary", new { id = genotypes.First().Id });
            }

            return View(genotypes);
        }

        /// <summary>
        /// Displays selection summary detail view for given genotype.
        /// </summary>
        /// <param name="id">
        /// Id of genotype to display selection summary for.
        /// </param>
        /// <returns>
        /// Detail view for selection summary.
        /// </returns>
        public ActionResult Detail(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SelectionSummaryViewModel ssvm = s_repo.GetSelectionSummary(id.Value);
            if (ssvm == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenusId = m_repo.GetGenotype(id.Value).Family.GenusId;
            ViewBag.FlatTypes = new SelectList(m_repo.GetFlatTypes(), "Id", "Label");
            return View(ssvm);
        }
        [HttpPost]
        public ActionResult Detail(int? filterId, string filter)
        {
            Genotype genotypeSearched = null;
            SelectionSummaryViewModel ssvm = null;

            int genusId = SessionManager.GetGenusId().Value;

            if (!filterId.HasValue && !filter.IsNullOrWhiteSpace())
            {
                var genotypesResult = m_repo.GetGenotypes(filter, genusId, 2);
                if (genotypesResult.Count() == 1)
                {
                    genotypeSearched = genotypesResult.First();
                }
            }
            else if(filterId.HasValue)
            {
                genotypeSearched = m_repo.GetGenotype(filterId.Value);
            }

            if (genotypeSearched != null)
            {
                ssvm = s_repo.GetSelectionSummary(genotypeSearched.Id);
            }

            if(ssvm == null)
            {
                return RedirectToAction("Index", new { filter = filter });
            }
            ViewBag.GenusId = genusId;

            ViewBag.FlatTypes = new SelectList(m_repo.GetFlatTypes(), "Id", "Name");
            return View(ssvm);

        }
        [HttpGet]
        public ActionResult GetCrossSelections(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vm = s_repo.GetCrossSelections(id.Value);
            return PartialView("_GenotypeSummaryView", vm);
        }


        [HttpPost]
        public ActionResult EditGenotypeNote(int genotypeId, string genotypeNote)
        {
            bool error = false;
            string message = string.Empty;

            try
            {
                s_repo.UpdateGenotypeNote(genotypeId, genotypeNote);

            }
            catch (AccessionException e)
            {
                error = true;
                message = e.Message;
            }

            return Json(new { Error = error, Message = message });
        }



        [HttpPost]
        public ActionResult EditGenotypePloidy(int genotypeId, string ploidy)
        {
            bool error = false;
            string message = string.Empty;

            try
            {
                s_repo.UpdateGenotypePloidy(genotypeId, ploidy);

            }
            catch (AccessionException e)
            {
                error = true;
                message = e.Message;
            }

            return Json(new { Error = error, Message = message });
        }



        [HttpPost]
        public ActionResult EditNote(Note note)
        {
            Genotype genotype = m_repo.GetGenotype(note.GenotypeId);
            if (genotype == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                m_repo.SaveNote(note);

            }

            return PartialView("~/Areas/Accessions/Views/SelectionSummary/EditorTemplates/EditNote.cshtml", note); ;
        }

        [HttpPost]
        public ActionResult CreateNote(Note note)
        {
            ActionResult result;
            if (note == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Genotype genotype = m_repo.GetGenotype(note.GenotypeId);
            if (genotype == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                m_repo.SaveNote(note);
                result = PartialView("~/Areas/Accessions/Views/SelectionSummary/EditorTemplates/EditNote.cshtml", note);
            }
            else
            {
                result = PartialView("~/Areas/Accessions/Views/SelectionSummary/EditorTemplates/EditNewNote.cshtml", note);
            }
            return result;
        }

        [HttpPost]
        public ActionResult EditFlatNote(FlatNote note)
        {
            Genotype genotype = m_repo.GetGenotype(note.GenotypeId);
            if (genotype == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.FlatTypes = new SelectList(m_repo.GetFlatTypes(), "Id", "Name");
            if (ModelState.IsValid)
            {
                m_repo.SaveFlatNote(note);
            }
            return PartialView("~/Areas/Accessions/Views/SelectionSummary/EditorTemplates/EditFlatNote.cshtml", note);
        }

        [HttpPost]
        public ActionResult CreateFlatNote(FlatNote note)
        {
            if (note == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genotype genotype = m_repo.GetGenotype(note.GenotypeId);
            if (genotype == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.FlatTypes = new SelectList(m_repo.GetFlatTypes(), "Id", "Name");
            if (ModelState.IsValid)
            {
                m_repo.SaveFlatNote(note);
                return PartialView("~/Areas/Accessions/Views/SelectionSummary/EditorTemplates/EditFlatNote.cshtml", note);
            }
            return PartialView("~/Areas/Accessions/Views/SelectionSummary/EditorTemplates/EditNewFlatNote.cshtml", note);
        }

        public ActionResult QuestionsModal(Question question)
        {
            return PartialView("QuestionsModal");
        }

        [HttpPost]
        public ActionResult SaveComments(MapComponentSummaryViewModel viewModel)
        {
            ActionResult view = null;
            MapComponentYears mapcompyears = m_repo.GetMapComponentYear(viewModel.Id);
            mapcompyears.Comments = viewModel.Comments;

            try
            {
                m_repo.SaveMapComponentYearComment(mapcompyears);

            }
            catch (Exception e)
            {
                view = Json(new { Error = true, Message = e.Message });
            }

            ViewBag.FlatTypes = new SelectList(m_repo.GetFlatTypes(), "Id", "Name");

            return view;

        }

        [HttpPost]
        public ActionResult SaveFates(MapComponentSummaryViewModel viewModel)
        {
            ActionResult view = null;
            try
            {
                m_repo.UpdateMapComponentFates(viewModel.Fates, viewModel.Id);
            }
            catch (Exception e)
            {
                view = Json(new { Error = true, Message = e.Message });
            }

            ViewBag.FlatTypes = new SelectList(m_repo.GetFlatTypes(), "Id", "Name");

            return view;
        }



    }
}