using System.Collections.Generic;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;

namespace KPZ_Restaurant_REST_API.Services
{
    public interface IOrderService
    {
        Task<Order> CreateNewOrder(Order newOrder, int restaurantId);
        Task<IEnumerable<OrderedProducts>> AddOrderedProducts(List<OrderedProducts> orderedProducts, int restaurantId);
        Task<IEnumerable<Order>> GetAllOrders(int restaurantId);
        Task<IList<OrderedProducts>> GetOrderedProducts(int orderId, int restaurantId);
        Task<OrderedProducts> UpdateOrderedProduct(OrderedProducts orderedProduct, int restaurantId);
        Task<Order> UpdateOrder(Order order, int restaurantId);
    }
}