using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.Web.Mvc;
using System.Threading.Tasks;
using Usda.PlantBreeding.Core.Interfaces;
using Usda.PlantBreeding.Core.Models;
using Usda.PlantBreeding.Data.DataAccess;
using BSGUtils;
namespace Usda.PlantBreeding.Core.Infrastructure
{
    public class OrdersUOW : IOrdersUOW
    {
        private IPlantBreedingRepo u_repo;
        public OrdersUOW() : this(new PlantBreedingRepo()) { }
        public OrdersUOW(IPlantBreedingRepo repo)
        {
            u_repo = repo;
        }

        public void CreateOrder(OrderViewModel order)
        {
            SaveOrder(order);
        }

        public void DeleteOrderProduct(OrderProductViewModel orderProduct)
        {
            var op = OrderProductViewModel.ToOrderProduct(orderProduct);
            u_repo.DeleteOrderProduct(op);
        }

        public OrderViewModel GetOrder(int id)
        {
            var order = u_repo.GetOrder(o => o.Id == id);
            if (order == null)
            {
                return null;
            }

            return OrderViewModel.Create(order);
        }

        public OrdersEditViewModel GetOrderEditPageVM(int id)
        {
            var orderVM = GetOrder(id);
            if (orderVM == null)
            {
                return null;
            }
            var materials = u_repo.GetMaterials().ToList();
            var orderEditVM = new OrdersEditViewModel
            {
                Materials = materials,
                Order = orderVM,
                OrderId = id
            };

            return orderEditVM;
        }

        public List<OrderProductViewModel> GetOrderProducts(int orderId)
        {
            var products = u_repo.GetOrderProducts(op => op.OrderId == orderId).Select(op => OrderProductViewModel.Create(op)).ToList();
            return products;
        }

        public List<OrderViewModel> GetOrders(int genusId)
        {
            var orders = u_repo.GetOrders(o => o.GenusId == genusId).Select(o => OrderViewModel.Create(o)).ToList();
            return orders;
        }

        public void SaveOrder(OrderViewModel order)
        {
            var newOrder = OrderViewModel.ToOrder(order);
            if (newOrder.Genus == null) newOrder.Genus = u_repo.GetGenus(newOrder.GenusId);
            if (newOrder.Location == null && newOrder.LocationId.HasValue)
            {
                newOrder.Location = u_repo.GetLocation(newOrder.LocationId.Value);
            }

            if (newOrder.Location != null && newOrder.Location.PrimaryContactId.HasValue)
            {
                newOrder.Grower = u_repo.GetGrower(newOrder.Location.PrimaryContactId.Value);
            }
            else if(newOrder.Grower == null)
            {
                newOrder.Grower = u_repo.GetGrower(newOrder.GrowerId);
            }

            u_repo.SaveOrder(newOrder);
            var newOrderVm = OrderViewModel.Create(newOrder);
            newOrderVm.CopyTo(order);
        }

        public void SaveOrderProduct(OrderProductViewModel orderProduct)
        {
            var op = OrderProductViewModel.ToOrderProduct(orderProduct);
            op.Genotype = u_repo.GetGenotype(op.GenotypeId);
            op.Material = u_repo.GetMaterials().SingleOrDefault(m => m.Id == op.MaterialId);
            u_repo.SaveOrderProduct(op);
            var savedVM = OrderProductViewModel.Create(op);
            savedVM.CopyTo(orderProduct);
        }
    }
}
