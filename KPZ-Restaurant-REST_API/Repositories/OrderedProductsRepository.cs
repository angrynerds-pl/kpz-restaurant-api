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
            if (orderedProductToDelete != null)
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
            return await _context.OrderedProducts.Where(o => o.Id == id && o.Order.RestaurantId == restaurantId).FirstOrDefaultAsync();
        }

        public async Task<IList<OrderedProducts>> GetOrderedProducts(int orderId, int restaurantId)
        {
            return await _context.OrderedProducts.Where(o => o.OrderId == orderId && o.Order.RestaurantId == restaurantId).Include(o => o.Product).ToListAsync();
        }

        public async Task<IList<OrderedProducts>> GetServedProducts(int orderId, int restaurantId)
        {
            return await _context.OrderedProducts.Where(o => o.OrderId == orderId && o.Order.RestaurantId == restaurantId && o.Status == "SERVED").Include(o => o.Product).ToListAsync();
        }

        public async Task<IEnumerable<SelledProduct>> GetTopSellingProducts(int restaurantId)
        {
            return await _context.OrderedProducts.Where(o => o.Order.RestaurantId == restaurantId)
                .GroupBy(o => o.ProductId)
                .Select(group => new SelledProduct { ProductName = _context.OrderedProducts.Where(p => p.Id == group.Key).FirstOrDefault().Product.Name, Quantity = group.Count() })
                .OrderByDescending(o => o.Quantity).Take(5).ToListAsync();
        }

        public async Task<IEnumerable<SelledProduct>> GetWorstSellingProducts(int restaurantId)
        {
            return await _context.OrderedProducts.Where(o => o.Order.RestaurantId == restaurantId)
                .GroupBy(o => o.ProductId)
                .Select(group => new SelledProduct { ProductName = _context.OrderedProducts.Where(p => p.Id == group.Key).FirstOrDefault().Product.Name, Quantity = group.Count() })
                .OrderBy(o => o.Quantity).Take(5).ToListAsync();
        }

        public async Task<bool> OrderedProductCorrect(OrderedProducts orderedProduct, int restaurantId)
        {
            return await _context.Orders.AnyAsync(o => o.Id == orderedProduct.OrderId && o.RestaurantId == restaurantId)
                && await _context.Products.AnyAsync(p => p.Id == orderedProduct.ProductId && p.RestaurantId == restaurantId);
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
            }
            else
                return null;
        }
    }
}
