using KPZ_Restaurant_REST_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Repositories
{
    public interface IOrderedProductsRepository : IRestaurantGeneric<OrderedProducts>
    {
        Task<bool> OrderedProductCorrect(OrderedProducts orderedProduct);
        Task<IList<OrderedProducts>> GetOrderedProducts(int orderId);
        Task<OrderedProducts> UpdateOrderedProduct(OrderedProducts orderedProduct);
    }
}
