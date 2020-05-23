using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;
using KPZ_Restaurant_REST_API.Repositories;

namespace KPZ_Restaurant_REST_API.Services
{
    public class OrderService : IOrderService
    {
        private IOrdersRepository _ordersRepo;
        private IOrderedProductsRepository _orderedProductsRepo;
        private IIncomeByMonthRepository _incomeByMonthRepo;

        public OrderService(IOrdersRepository ordersRepo, IOrderedProductsRepository productsInOrderRepo, IIncomeByMonthRepository incomeByMonthRepo)
        {
            _ordersRepo = ordersRepo;
            _orderedProductsRepo = productsInOrderRepo;
            _incomeByMonthRepo = incomeByMonthRepo;
        }

        public async Task<Order> CreateNewOrder(Order newOrder, int restaurantId)
        {
            newOrder.RestaurantId = restaurantId;
            var orderCorrect = await _ordersRepo.OrderCorrect(newOrder);
            if (orderCorrect)
            {
                await _ordersRepo.Add(newOrder);
                await _ordersRepo.SaveAsync();
                await OrderIncreaseIncome(newOrder, restaurantId);

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
                await OrderIncreaseIncome(order, restaurantId);
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
                    if (updatedProduct != null)
                        updatedProducts.Add(updatedProduct);
                }
            }

            return updatedProducts;

        }

        public async Task<IEnumerable<OrderedProducts>> GetServedProducts(int orderId, int restaurantId)
        {
            var servedProducts = await _orderedProductsRepo.GetServedProducts(orderId, restaurantId);
            if (servedProducts == null || servedProducts.Count == 0)
                return null;
            else
                return servedProducts;
        }

        public async Task<IEnumerable<Order>> GetOrdersByOrderDate(int year, int month, int day, int restaurantId)
        {
            var serachedDate = new DateTime(year, month, day);
            return await _ordersRepo.GetOrdersByDate(serachedDate, restaurantId);
        }

        public async Task<IEnumerable<Order>> GetOrdersInProgress(int restaurantId)
        {
            return await _ordersRepo.GetOrdersInProgress(restaurantId);
        }

        public async Task<Order> UpdateOrderStatus(int orderId, string status, int restaurantId)
        {
            var statuses = new List<string>() { "PENDING", "PAID", "IN_PROGRESS", "SERVED" };

            var orderToUpdate = await _ordersRepo.GetOrderById(orderId, restaurantId);
            if (orderToUpdate != null && statuses.Contains(status))
            {
                orderToUpdate.Status = status;
                var updatedOrder = await _ordersRepo.UpdateOrder(orderToUpdate);

                await OrderIncreaseIncome(orderToUpdate, restaurantId);

                return updatedOrder;
            }
            else
                return null;
        }

        public async Task<IEnumerable<Order>> GetOrdersByOrderDateRange(DateRange dateRange, int restaurantId)
        {
            return await _ordersRepo.GetOrdersByDateRange(dateRange, restaurantId);
        }

        public async Task<IEnumerable<Order>> GetOrdersFromLastMonth(int restaurantId)
        {
            DateRange lastMonth = new DateRange() { StartDate = DateTime.Now.AddMonths(-1), EndDate = DateTime.Now };
            return await _ordersRepo.GetOrdersByDateRange(lastMonth, restaurantId);
        }

        public async Task<IEnumerable<Order>> GetOrdersFromLastWeek(int restaurantId)
        {
            DateRange lastMonth = new DateRange() { StartDate = DateTime.Now.AddDays(-7), EndDate = DateTime.Now };
            return await _ordersRepo.GetOrdersByDateRange(lastMonth, restaurantId);
        }

        private async Task OrderIncreaseIncome(Order order, int restaurantId)
        {
            if (order.Status == "PAID")
            {
                decimal fullPrice = 0M;
                var orderFromDatabase = await _ordersRepo.GetOrderById(order.Id, restaurantId);
                if (orderFromDatabase.OrderedProducts != null)
                {
                    foreach (var product in orderFromDatabase.OrderedProducts)
                        fullPrice += product.Product.Price;

                    await _incomeByMonthRepo.IncreaseIncome(restaurantId, fullPrice, orderFromDatabase.OrderDate.Month.ToString());
                    await _incomeByMonthRepo.SaveAsync();
                }
            }
        }
    }
}