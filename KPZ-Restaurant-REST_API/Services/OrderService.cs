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
                var updatedProduct = await _orderedProductsRepo.UpdateOrderedProduct(orderedProduct);
                return updatedProduct;
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
                var updatedOrder = await _ordersRepo.UpdateOrder(order);
                return updatedOrder;
            }
            else
                return null;
        }

        public async Task<IEnumerable<Order>> GetOrdersForTable(int tableId, int restaurantId)
        {
            return await _ordersRepo.GetOrdersForTable(tableId, restaurantId);
        }

        public async Task<Order> GetOrderById(int orderId, int restaurantId)
        {
            return await _ordersRepo.GetOrderById(orderId, restaurantId);
        }

        public async Task<OrderedProducts> DeleteOrderedProduct(int orderedProductId, int restaurantId)
        {
            var deletedOrderedProduct = await _orderedProductsRepo.DeleteOrderedProductById(orderedProductId, restaurantId);
            if (deletedOrderedProduct != null)
            {
                await _orderedProductsRepo.SaveAsync();
                return deletedOrderedProduct;
            }
            else
                return null;
        }

        public async Task<IEnumerable<OrderedProducts>> UpdateManyOrderedProducts(List<OrderedProducts> orderedProducts, int restaurantId)
        {
            var updatedProducts = new List<OrderedProducts>();

            foreach (var orderedProduct in orderedProducts)
            {
                var orderedProductCorrect = await _orderedProductsRepo.OrderedProductCorrect(orderedProduct, restaurantId);

                if (orderedProductCorrect)
                {
                    var updatedProduct = await _orderedProductsRepo.UpdateOrderedProduct(orderedProduct);
                    if(updatedProduct != null)
                        updatedProducts.Add(updatedProduct);
                }
            }

            return updatedProducts;

        }
    }
}