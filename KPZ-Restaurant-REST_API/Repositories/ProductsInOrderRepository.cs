using KPZ_Restaurant_REST_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Repositories
{
    public class ProductsInOrderRepository : RestaurantGeneric<ProductInOrder>, IProductsInOrderRepository
    {
        RestaurantContext _context;

        public ProductsInOrderRepository(RestaurantContext context) : base(context)
        {
            _context = context;
        }
    }
}
