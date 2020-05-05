using System.Collections.Generic;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;
using KPZ_Restaurant_REST_API.Repositories;

namespace KPZ_Restaurant_REST_API.Services
{
    public class OrderService : IOrderService
    {
        private IOrdersRepository _ordersRepo;
        private IOrderedProductsRepository _orderedProductsRepo;

        public OrderService(IOrdersRepository ordersRepo, IOrderedProductsRepository productsInOrderRepo)
        {
            _ordersRepo = ordersRepo;
            _orderedProductsRepo = productsInOrderRepo;
        }

        public async Task<Order> CreateNewOrder(Order newOrder)
        {
            var orderCorrect = await _ordersRepo.OrderCorrect(newOrder);
            if (orderCorrect)
            {
                await _ordersRepo.Add(newOrder);
                await _ordersRepo.SaveAsync();
                return newOrder;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<OrderedProducts>> AddOrderedProducts(List<OrderedProducts> orderedProducts)
        {
            foreach (var orderedProduct in orderedProducts)
            {
                var orderedProductCorrect = await _orderedProductsRepo.OrderedProductCorrect(orderedProduct);
                if (orderedProductCorrect)
                    await _orderedProductsRepo.Add(orderedProduct);
                else
                    return null;
            }

            await _ordersRepo.SaveAsync();
            return orderedProducts;
        }

        public async Task<IEnumerable<Order>> GetAllOrders(int restaurantId)
        {
            return await _ordersRepo.GetAllOrders(restaurantId);
        }

        public async Task<IList<OrderedProducts>> GetOrderedProducts(int orderId)
        {
            var orderedProducts = await _orderedProductsRepo.GetOrderedProducts(orderId);
            if (orderedProducts == null || orderedProducts.Count == 0)
                return null;
            else
                return orderedProducts;
        }
    }
}