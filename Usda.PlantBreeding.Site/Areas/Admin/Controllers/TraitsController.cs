using System.Linq;
using System.Net;
using System.Web.Mvc;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Data.Util;
using BSGUtils;
using System.Collections.Generic;
using System;
using Usda.PlantBreeding.Core.Models;

namespace Usda.PlantBreeding.Site.Areas.Admin.Controllers
{
    [ActionFilters.AuthActionFilters]
    public class TraitsController : Controller
    {
        private IPlantBreedingRepo m_repo;

        /// <summary>
        /// Default constructor called by the system.
        /// </summary>
        public TraitsController() : this(new PlantBreedingRepo()) { }

        /// <summary>
        /// Overloaded constructor to facilitate dependency injection.
        /// </summary>
        public TraitsController(IPlantBreedingRepo repo)
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

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }
            var genus = m_repo.GetGenus(id.Value);
            var traitViewModel = new TraitViewModel
                                        {
                                            Genus = genus,
                                            RetiredQuestions = genus.Questions.Where(t => t.Retired == true).OrderBy(t => t.Order),
                                            ActiveQuestions = genus.Questions.Where(t => t.Retired == false).OrderBy(t => t.Order)
                                        };

            return View(traitViewModel);
        }


        public ActionResult QuestionCreate(string questionText, int genusId, bool required, string labelText, int? InputTypeId)
        {
            if (ModelState.IsValid)
            {
                Question question = new Question
                {
                    Text = questionText,
                    GenusId = genusId,
                    Genus = m_repo.GetGenus(genusId),
                    Required = required,
                    Retired = false,
                    Label = labelText,
                    Order = m_repo.GetQuestions(q => !q.Retired.Value && q.GenusId == genusId).Max(q => q.Order) + 1,
                    InputTypeId = InputTypeId
                };
                m_repo.SaveQuestion(question);
                return RedirectToAction("Details", new { id = genusId });
            }
            return RedirectToAction("Index");
        }

        public ActionResult QuestionRetire(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = m_repo.GetQuestion(id.Value);
            if (question == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            // update question
            question.Retired = true;
            question.Order = m_repo.GetQuestions(q => q.GenusId == question.GenusId).Max(q => q.Order) + 1;
            m_repo.SaveQuestion(question);
            return RedirectToAction("Details", new { id = question.GenusId });
        }

        public ActionResult QuestionUnRetire(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = m_repo.GetQuestion(id.Value);
            if (question == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // add question to end of unretired questions
            question.Retired = false;
            question.Order = m_repo.GetQuestions(q => !q.Retired.Value && q.GenusId == question.GenusId).Max(q => q.Order) + 1;
            m_repo.SaveQuestion(question);
            return RedirectToAction("Details", new { id = question.GenusId });
        }

        public ActionResult QuestionUpdate(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = m_repo.GetQuestion(id.Value);
            if (question == null)
            {
                return HttpNotFound();
            }
            // add select list of question orders to view bag
            var questions = m_repo
                .GetQuestions(q => q.Retired == question.Retired && q.GenusId == question.GenusId)
                .OrderBy(q => q.Order)
                .Select(q => new { Order = q.Order});

            int fromOrder = 1;
            int toOrder = 1;
            if (question.Retired == true)
            {
                fromOrder = questions.Min(t => t.Order);
                toOrder = questions.Max(t => t.Order) - fromOrder + 1;
            }
            else
            {
                fromOrder = 1;
                toOrder = questions.Max(t => t.Order) ;
            }

            var items = Enumerable.Range(fromOrder, toOrder).OrderBy(t => t).Select(t =>  new { Order = t.ToString()}).ToList();
            var inputTypes = m_repo.InputTypes();
            ViewBag.Orders = new SelectList(items, "Order", "Order");
            ViewBag.InputTypes = new SelectList(inputTypes, "Id", "Text");

            // return view
            return PartialView("_QuestionEdit", question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QuestionUpdate(Question updatedQuestion)
        {
            if (updatedQuestion == null)
            {
                // What to do here?
                throw new ArgumentNullException("updatedQuestion");
            }
            if (ModelState.IsValid)
            {
           
                m_repo.SaveQuestion(updatedQuestion);
                return Json(new { success = true });
            }
            return PartialView("_QuestionEdit", updatedQuestion);
        }
    }
}
