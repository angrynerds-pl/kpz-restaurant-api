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

        public async Task<Order> CreateNewOrder(Order newOrder, int restaurantId)
        {
            newOrder.RestaurantId = restaurantId;
            var orderCorrect = await _ordersRepo.OrderCorrect(newOrder);
            if (orderCorrect)
            {
                await _ordersRepo.Add(newOrder);
                await _ordersRepo.SaveAsync();
                return newOrder;
            }
            else
                return null;
        }

        public async Task<IEnumerable<OrderedProducts>> AddOrderedProducts(List<OrderedProducts> orderedProducts, int restaurantId)
        {
            var productsToAdd = new List<OrderedProducts>();
            foreach (var product in orderedProducts)
            {
                var orderedProductCorrect = await _orderedProductsRepo.OrderedProductCorrect(product, restaurantId);
                if (orderedProductCorrect)
                {
                    productsToAdd.Add(product);
                }
                else
                    return null;
            }

            await _orderedProductsRepo.AddRange(productsToAdd);
            await _orderedProductsRepo.SaveAsync();
            return orderedProducts;
        }

        public async Task<IEnumerable<Order>> GetAllOrders(int restaurantId)
        {
            return await _ordersRepo.GetAllOrders(restaurantId);
        }

        public async Task<IList<OrderedProducts>> GetOrderedProducts(int orderId, int restaurantId)
        {
            var orderedProducts = await _orderedProductsRepo.GetOrderedProducts(orderId, restaurantId);
            if (orderedProducts == null || orderedProducts.Count == 0)
                return null;
            else
                return orderedProducts;
        }

        public async Task<OrderedProducts> UpdateOrderedProduct(OrderedProducts orderedProduct, int restaurantId)
        {
            var orderedProductCorrect = await _orderedProductsRepo.OrderedProductCorrect(orderedProduct, restaurantId);
            if (orderedProductCorrect)
            {
                await _orderedProductsRepo.UpdateOrderedProduct(orderedProduct);
                await _orderedProductsRepo.SaveAsync();
                return orderedProduct;
            }
            else
                return null;

        }

        public async Task<Order> UpdateOrder(Order order, int restaurantId)
        {
            order.RestaurantId = restaurantId;
            var orderCorrect = await _ordersRepo.OrderCorrect(order);
            if (orderCorrect)
            {
                await _ordersRepo.UpdateOrder(order);
                await _ordersRepo.SaveAsync();
                return order;
            }
            else
                return null;
        }
    }
}