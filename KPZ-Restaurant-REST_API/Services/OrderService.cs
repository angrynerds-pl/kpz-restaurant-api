using System.Collections.Generic;
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

        public Order CreateNewOrder(Order newOrder)
        {
            if (_ordersRepo.OrderCorrect(newOrder))
            {
                var addedEntity = _ordersRepo.Create(newOrder);
                _ordersRepo.Save();
                return addedEntity;
            }
            else
            {
                return null;
            }
        }
    }
}