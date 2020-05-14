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

        public async Task<OrderedProducts> DeleteOrderedProductById(int orderedProductId, int restaurantId)
        {
            var orderedProductToDelete = await _context.OrderedProducts.Where(op => op.Id == orderedProductId && op.Order.RestaurantId == restaurantId && op.DeletedAt == null).FirstOrDefaultAsync();
            if(orderedProductToDelete != null)
            {
                orderedProductToDelete.DeletedAt = DateTime.Now;
                _context.OrderedProducts.Update(orderedProductToDelete);
                return orderedProductToDelete;
            }
            else
                return null;
        }

        public async Task<OrderedProducts> GetOrderedProductById(int id, int restaurantId)
        {
            return await _context.OrderedProducts.Where(o => o.Id == id && o.Order.RestaurantId == restaurantId  && o.DeletedAt == null).FirstOrDefaultAsync();
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

        public async Task<OrderedProducts> UpdateOrderedProduct(OrderedProducts orderedProduct)
        {
            var entity = await _context.OrderedProducts.FindAsync(orderedProduct.Id);
            if (entity != null)
            {
                entity.OrderId = orderedProduct.OrderId;
                entity.ProductId = orderedProduct.ProductId;
                entity.Status = orderedProduct.Status;
                _context.OrderedProducts.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            } else 
                return null;
        }
    }
}
