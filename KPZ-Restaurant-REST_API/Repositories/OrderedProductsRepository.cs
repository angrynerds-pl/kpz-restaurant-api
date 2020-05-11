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
        
        public async Task<IList<OrderedProducts>> GetOrderedProducts(int orderId, int restaurantId)
        {
            return await _context.OrderedProducts.Where(o => o.OrderId == orderId && o.Order.RestaurantId == restaurantId && o.DeletedAt == null).Include(o => o.Product).ToListAsync();
        }
        
        public async Task<bool> OrderedProductCorrect(OrderedProducts orderedProduct, int restaurantId)
        {
            return await _context.Orders.AnyAsync(o => o.Id == orderedProduct.OrderId && o.RestaurantId == restaurantId && o.DeletedAt == null)
                && await _context.Products.AnyAsync(p => p.Id == orderedProduct.ProductId && p.RestaurantId == restaurantId && p.DeletedAt == null);
        }
    }
}
