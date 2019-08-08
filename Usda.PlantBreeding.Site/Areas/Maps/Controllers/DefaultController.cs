using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Site.Models;
using Usda.PlantBreeding.Site.Models.Translators;
using BSGUtils;
using PagedList;
using System.Configuration;
using Usda.PlantBreeding.Core.Models;
using Usda.PlantBreeding.Core.Interfaces;
using Usda.PlantBreeding.Core.Infrastructure;


namespace Usda.PlantBreeding.Site.Areas.Maps.Controllers
{
    [ActionFilters.AuthActionFilters]
    public class DefaultController : Controller
    {
        private IPlantBreedingRepo m_repo;
        private IMapUOW map_repo;
        public DefaultController() : this(new PlantBreedingRepo()) { }

        public DefaultController(IPlantBreedingRepo repo)
        {
            m_repo = repo;
            map_repo = new MapUow(repo);
        }

        [ActionFilters.GenusActionFilter]
        public ActionResult Index(int page = 1)
        {
            int pageSize = Properties.Settings.Default.PageSize;
            int genusId = SessionManager.GetGenusId().Value;
            IQueryable<Map> maps = m_repo.GetQueryableMaps(m => m.GenusId == genusId && m.Retired == false)
                                                                    .OrderByDescending(m => m.PlantingYear)
                                                                    .ThenByDescending(m => m.Id);

            if (maps.IsNullOrEmpty())
            {
                return RedirectToAction("Create");
            }

            var pageOfMaps = maps.ToPagedList(page, pageSize);
            ViewBag.OnePageOfProducts = pageOfMaps;

            return View(maps);
        }

        [ActionFilters.GenusActionFilter]
        public ActionResult Retired(int page = 1)
        {
            int pageSize = Properties.Settings.Default.PageSize;
            int genusId = SessionManager.GetGenusId().Value;
            IQueryable<Map> maps = m_repo.GetQueryableMaps(m => m.GenusId == genusId && m.Retired == true)
                                                                    .OrderByDescending(m => m.PlantingYear)
                                                                    .ThenByDescending(m => m.Id);

            var pageOfMaps = maps.ToPagedList(page, pageSize);
            ViewBag.OnePageOfProducts = pageOfMaps;

            return View(maps);
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Map map = m_repo.GetMap(id.Value);

            if (map == null)
            {
                return HttpNotFound();
            }

            ViewBag.MapComponents = m_repo.GetMapComponents(map.Id);
        
            var crossTypes = m_repo.GetCrossTypes().Where(t => t.GenusId == map.GenusId && t.Retired == false);
            ViewBag.CrossTypes = new SelectList(crossTypes, "Id", "Name");
            map.Questions = map.Questions.OrderBy(q => q.Order).ToList();
            map.MapComponents = map.MapComponents.OrderBy(t => t.Row).ThenBy(t => t.PlantNum).ToList();
            return View(map);
        }

        public ActionResult RetireMap(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Map map = m_repo.GetMap(id.Value);

            if (map == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                map.Retired = true;
                m_repo.SaveMap(map);

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public ActionResult UnRetireMap(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Map map = m_repo.GetMap(id.Value);

            if (map == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                map.Retired = false;
                m_repo.SaveMap(map);

                return RedirectToAction("Retired");
            }

            return RedirectToAction("Retired");
        }

        [ActionFilters.GenusActionFilter]
      
        public ActionResult Create()
        {

            int genusId =SessionManager.GetGenusId().Value;
            List<Question> questions = m_repo.GetQuestions(q => q.Retired == false && q.GenusId == genusId).ToList();
            var genus = m_repo.GetGenus(genusId);
            MapQuestionListViewModel model = MapQuestionViewModelTranslator.ToMapQuestionListViewModel(questions, genus, genus.DefaultPlantsInRep);
            var crossTypes = m_repo.GetCrossTypes().Where(t => t.GenusId == model.Map.GenusId && t.Retired == false);

            ViewBag.CrossTypes = new SelectList(crossTypes, "Id", "Name");
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MapQuestionListViewModel model)
        {
            int genusId =SessionManager.GetGenusId().Value;
    
            if (ModelState.IsValid)
            {
                map_repo.CreateMap(model);
                return RedirectToAction("Details", new { id = model.Map.Id });
            }
            if (model.Map != null & model.Map.LocationId.HasValue)
            {
                model.Map.Location = m_repo.GetLocation(model.Map.LocationId.Value);
            }
            var crossTypes = m_repo.GetCrossTypes().Where(t => t.GenusId == model.Map.GenusId && t.Retired == false);
            ViewBag.CrossTypes = new SelectList(crossTypes, "Id", "Name", model.Map.CrossTypeId);

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Map map = m_repo.GetMap(id.Value);
            if (map == null)
            {
                return HttpNotFound();
            }
            List<Question> availQuestions = m_repo.GetQuestions(q => q.Retired == false && q.GenusId == map.GenusId).ToList();
            MapQuestionListViewModel mapQuestion = map.ToMapQuestionListViewModel(availQuestions);
         
            ViewBag.Location = new SelectList(m_repo.GetLocations(), "Id", "Name");
            var crossTypes = m_repo.GetCrossTypes().Where(t => t.GenusId == map.GenusId && t.Retired == false);

            ViewBag.CrossTypes = new SelectList(crossTypes, "Id", "Name");

            return View(mapQuestion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MapQuestionListViewModel mapQuestionViewModel)
        {

            ActionResult result;
            if (ModelState.IsValid)
            {
                 Map map = mapQuestionViewModel.Map;

                IEnumerable<Question> selectedQuestions = mapQuestionViewModel.Questions.Where(q => q.isChecked).ToQuestion().ToList();
                IEnumerable<Question> mapQuestions = m_repo.GetQuestions(t => t.GenusId == map.GenusId);
                map.Genus = m_repo.GetGenus(map.GenusId);
                m_repo.SaveMap(map);

                map = m_repo.GetMap(map.Id);

                IEnumerable<Question> updatedQuestions = mapQuestions.Intersect(selectedQuestions, new QuestionComparer());
                map.Questions.Clear(); // needed to update questions
                map.Questions = updatedQuestions.ToList();
                m_repo.SaveMap(map);


                result = RedirectToAction("Details", new { id = map.Id });
            }
            else
            {
                if (mapQuestionViewModel.Map != null & mapQuestionViewModel.Map.LocationId.HasValue)
                {
                    mapQuestionViewModel.Map.Location = m_repo.GetLocation(mapQuestionViewModel.Map.LocationId.Value);
                }

                result = View(mapQuestionViewModel);
            }
            var crossTypes = m_repo.GetCrossTypes().Where(t => t.GenusId == mapQuestionViewModel.Map.GenusId && t.Retired == false);

            ViewBag.CrossTypes = new SelectList(crossTypes, "Id", "Name", mapQuestionViewModel.Map.CrossTypeId);


            return result;
        }

        public JsonResult SearchLocations(string term)
        {
            term = term.TrimAndRemoveDoubleSpaces();
            if (string.IsNullOrEmpty(term) || term.Length < 1)
            {
                return Json(new Object[] { }, JsonRequestBehavior.AllowGet);
            }
            int recordsToReturn = Properties.Settings.Default.RecordsToReturn;
            var locationList = map_repo.SearchLocations(term, recordsToReturn);
            return Json(locationList, JsonRequestBehavior.AllowGet);
        }

    }
}
