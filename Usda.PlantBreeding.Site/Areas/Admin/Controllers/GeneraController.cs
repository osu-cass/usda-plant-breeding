using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Data.Util;
using BSGUtils;
using System.Collections.Generic;

namespace Usda.PlantBreeding.Site.Areas.Admin.Controllers
{
    [ActionFilters.AuthActionFilters]
    public class GeneraController : Controller
    {
        private IPlantBreedingRepo m_repo;

        public GeneraController() : this(new PlantBreedingRepo()) { }

        public GeneraController(IPlantBreedingRepo repo)
        {
            m_repo = repo;
        }

        // GET: Genus
        public ActionResult Index()
        {
            IEnumerable<Genus> genus = m_repo.GetAllGenera();
            if (genus.IsNullOrEmpty())
            {
                return RedirectToAction("Create");
            }
            return View(genus.ToList());
        }

        // GET: Genus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genus genus = m_repo.GetGenus(id.Value);
            if (genus == null)
            {
                return HttpNotFound();
            }
            genus.Questions = genus.Questions.OrderBy(q => q.Order).ToList();
            return View(genus);
        }

        // GET: Genus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Genus genus)
        {
            // List has changed, repopulate genera list
            Session["genusSet"] = false;
            // Defaults are not set to null - not standard
            if (genus == null || genus.Name.IsNullOrWhiteSpace() || genus.Value.IsNullOrWhiteSpace())
            {
                return RedirectToAction("Create");
            }
            if (ModelState.IsValid)
            {
                m_repo.SaveGenus(genus);
                return RedirectToAction("Details", new { id = genus.Id });
            }
            return View(genus);
        }

        // GET: Genus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genus genus = m_repo.GetGenus(id.Value);
            if (genus == null)
            {
                return HttpNotFound();
            }
            return View(genus);
        }

        // POST: Genus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Genus genus)
        {
            // List has changed, repopulate genera list
            Session["genusSet"] = false;
            if (ModelState.IsValid)
            {
                m_repo.SaveGenus(genus);
                return RedirectToAction("Details", new { id = genus.Id });
            }
            return View(genus);
        }

        public ActionResult QuestionCreate(string questionText, int genusId, bool required, string labelText)
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
                    Order = m_repo.GetQuestions(q => !q.Retired.Value && q.GenusId == genusId).Max(q => q.Order) + 1
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
            // retire question and reorder unretired questions; note that questions must be a list
            List<Question> questions = m_repo.GetQuestions(q => !q.Retired.Value && q.GenusId == question.GenusId && q.Order > question.Order).ToList();
            foreach (var q in questions)
            {
                q.Order--;
            }
            // update question
            question.Retired = true;
            question.Order = short.MaxValue;
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
                .GetQuestions(q => !q.Retired.Value && q.GenusId == question.GenusId)
                .OrderBy(q => q.Order)
                .Select(q => new { Order = q.Order.ToString() });
            ViewBag.Orders = new SelectList(questions, "Order", "Order");
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
                /* 
                 * reorder questions pseudocode:
                 * 
                 * i = old order of question
                 * j = new order of question
                 * if (i == j)
                 *   do nothing
                 * else if (i < j)
                 *   shift from i+1 to j down by 1
                 * else
                 *   shift from j to i-1 up by 1
                 */
                Question question = m_repo.GetQuestion(updatedQuestion.Id);
                int i = question.Order; // old order
                int j = updatedQuestion.Order; // new order
                if (i < j)
                {
                    // shift questions down; note that questions must be a list
                    List<Question> questions = m_repo.GetQuestions(q => !q.Retired.Value && q.GenusId == question.GenusId && q.Order > i && q.Order <= j).ToList();
                    foreach (var q in questions)
                    {
                        q.Order--;
                        m_repo.SaveQuestion(q);
                    }
                }
                else if (j < i)
                {
                    // shift questions up; note that questions must be a list
                    List<Question> questions = m_repo.GetQuestions(q => !q.Retired.Value && q.GenusId == question.GenusId && q.Order >= j && q.Order < i).ToList();
                    foreach (var q in questions)
                    {
                        q.Order++;
                        m_repo.SaveQuestion(q);
                    }
                }
                // update question
                updatedQuestion.Genus = question.Genus; // m_repo.GetGenus(updatedQuestion.GenusId);
                m_repo.SaveQuestion(updatedQuestion);
                return Json(new { success = true });
            }
            return PartialView("_QuestionEdit", updatedQuestion);
        }

        public ActionResult Retire(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genus genus = m_repo.GetGenus(id.Value);
            if (genus == null)
            {
                return HttpNotFound();
            }
            genus.Retired = !genus.Retired;
            m_repo.SaveGenus(genus);
            return RedirectToAction("Index");
        }
    }
}
