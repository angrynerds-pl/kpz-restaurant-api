using KPZ_Restaurant_REST_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Repositories
{
    public interface IOrdersRepository: IRestaurantGeneric<Order>
    {
        Task<IEnumerable<Order>> GetAllOrders(int restaurantId);
        Task<bool> OrderCorrect(Order order);
        Task<Order> UpdateOrder(Order order);
        Task<IEnumerable<Order>> GetOrdersForTable(int tableId, int restaurantId);
        Task<Order> GetOrderById(int orderId, int restaurantId);
        Task<IEnumerable<Order>> GetOrdersByDate(DateTime serachedDate, int restaurantId);
    }
}
