using KPZ_Restaurant_REST_API.Models;

namespace KPZ_Restaurant_REST_API.Services
{
    public interface IOrderService
    {
        Order CreateNewOrder(Order newOrder);
        
    }
}