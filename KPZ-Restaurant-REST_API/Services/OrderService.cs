using System.Collections.Generic;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;
using KPZ_Restaurant_REST_API.Repositories;

namespace KPZ_Restaurant_REST_API.Services
{
    public class OrderService : IOrderService
    {
        private IOrdersRepository _ordersRepo;
        private IProductsInOrderRepository _productsInOrderRepo;

        public OrderService(IOrdersRepository ordersRepo, IProductsInOrderRepository productsInOrderRepo)
        {
            _ordersRepo = ordersRepo;
            _productsInOrderRepo = productsInOrderRepo;
        }

        public async Task<Order> CreateNewOrder(Order newOrder)
        {
            var orderCorrect = await _ordersRepo.OrderCorrect(newOrder);
            if (orderCorrect)
            {
                _ordersRepo.Create(newOrder);
                _ordersRepo.SaveAsync();
                return newOrder;
            }
            else
            {
                return null;
            }
        }
    }
}