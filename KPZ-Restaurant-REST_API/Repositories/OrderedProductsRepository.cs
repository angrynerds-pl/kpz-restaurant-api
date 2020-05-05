using KPZ_Restaurant_REST_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Repositories
{
    public class OrderedProductsRepository : RestaurantGeneric<OrderedProducts>, IOrderedProductsRepository
    {
        private RestaurantContext _context;

        public OrderedProductsRepository(RestaurantContext context) : base(context)
        {
            _context = context;
        }
        
        public async Task<IList<OrderedProducts>> GetOrderedProducts(int orderId)
        {
            return await _context.OrderedProducts.Where(o => o.OrderId == orderId).ToListAsync();
        }
        
        public async Task<bool> OrderedProductCorrect(OrderedProducts orderedProduct)
        {
            return await _context.Orders.AnyAsync(o => o.Id == orderedProduct.OrderId)
                && await _context.Products.AnyAsync(p => p.Id == orderedProduct.ProductId);
        }
    }
}
