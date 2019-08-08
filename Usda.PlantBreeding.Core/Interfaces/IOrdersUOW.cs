using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Core.Models;

namespace Usda.PlantBreeding.Core.Interfaces
{
    public interface IOrdersUOW
    {
        /// <summary>
        /// Gets a list of orders for the selected genus
        /// </summary>
        /// <returns></returns>
        List<OrderViewModel> GetOrders(int genusId);

        /// <summary>
        /// Gets an order with given id
        /// </summary>
        /// <returns>returns an order view model</returns>
        OrderViewModel GetOrder(int id);

        /// <summary>
        /// Gets an order with given id
        /// </summary>
        /// <returns>returns an order view model</returns>
        OrdersEditViewModel GetOrderEditPageVM(int id);

        /// <summary>
        /// Creates an order
        /// </summary>
        void CreateOrder(OrderViewModel order);

        void SaveOrder(OrderViewModel order);
        List<OrderProductViewModel> GetOrderProducts(int orderId);
        void SaveOrderProduct(OrderProductViewModel orderProduct);
        void DeleteOrderProduct(OrderProductViewModel orderProduct);
    }
}
