using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Core.Models;
using Usda.PlantBreeding.Core.Interfaces;
using Usda.PlantBreeding.Core.Infrastructure;
using Usda.PlantBreeding.Site.CustomAttributes;
using Newtonsoft.Json.Converters;

namespace Usda.PlantBreeding.Site.Areas.Accessions.Controllers
{
    [ActionFilters.AuthActionFilters]
    public class DistributionsController : Controller
    {
        private IOrdersUOW o_repo;
        public DistributionsController() : this(new OrdersUOW()) { }

        public DistributionsController(IOrdersUOW repo)
        {
            o_repo = repo;
        }

        [ActionFilters.GenusActionFilter]
        public ActionResult Index()
        {
            int genusId = SessionManager.GetGenusId().Value;
            var orders = o_repo.GetOrders(genusId);
            return View(orders);
        }

        [ActionFilters.GenusActionFilter]
        [HttpGet]
        public ActionResult Create()
        {
            int genusId = SessionManager.GetGenusId().Value;
            var vm = new CreateOrderViewModel { GenusId = genusId };
            return View(vm);
        }


        [HttpPost]
        public ActionResult SaveOrder(OrderViewModel order = null)
        {
            if (order == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            try
            {
                o_repo.SaveOrder(order);
                JsonNetResult jsonNetResult = new JsonNetResult();
                jsonNetResult.Data = order;
                jsonNetResult.SerializerSettings.DateFormatString = "yyyy-MM-dd";
                return jsonNetResult;
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, e.Message);
            }

        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var orderEdit = o_repo.GetOrderEditPageVM(id.Value);

            if (orderEdit == null || orderEdit.Order == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return View(orderEdit);
        }

        [HttpGet]
        public ActionResult GetOrder(int? id)
        {
            if (!id.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var orderEdit = o_repo.GetOrderEditPageVM(id.Value);

            if (orderEdit == null || orderEdit.Order == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            try
            {
                JsonNetResult jsonNetResult = new JsonNetResult();
                jsonNetResult.Data = orderEdit.Order;
                jsonNetResult.SerializerSettings.DateFormatString = "yyyy-MM-dd";
                return jsonNetResult;

            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, e.Message);
            }

        }

        [HttpGet]
        public ActionResult GetOrderProducts(int? orderId)
        {
            if (!orderId.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            try
            {
                var orderProducts = o_repo.GetOrderProducts(orderId.Value);
                JsonNetResult jsonNetResult = new JsonNetResult();
                jsonNetResult.Data = orderProducts;
                jsonNetResult.SerializerSettings.DateFormatString = "yyyy-MM-dd";
                return jsonNetResult;

            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, e.Message);
            }

        }

        [HttpPost]
        public ActionResult SaveOrderProduct(OrderProductViewModel product)
        {
            if (product == null ) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
           
            try
            {
                o_repo.SaveOrderProduct(product);
                JsonNetResult jsonNetResult = new JsonNetResult();
                jsonNetResult.Data = product;
                jsonNetResult.SerializerSettings.DateFormatString = "yyyy-MM-dd";
                return jsonNetResult;
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, e.Message);
            }

        }

        [HttpPost]
        public ActionResult DeleteOrderProduct(OrderProductViewModel product)
        {
            if (product == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            try
            {
                o_repo.DeleteOrderProduct(product);
                return Json(new { text= $"order product {product.Id} was deleted" });
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, e.Message);
            }

        }

    }
}