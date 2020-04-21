using System.Collections.Generic;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;

namespace KPZ_Restaurant_REST_API.Services
{
    public interface IOrderService
    {
        Task<Order> CreateNewOrder(Order newOrder);
        Task<IEnumerable<OrderedProducts>> AddOrderedProducts(List<OrderedProducts> orderedProducts);
        Task<IEnumerable<Order>> GetAllOrders();
        Task<IList<OrderedProducts>> GetOrderedProducts(int orderId);
    }
}