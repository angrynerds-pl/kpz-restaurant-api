using KPZ_Restaurant_REST_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Repositories
{
    public class ProductsRepository : RestaurantGeneric<Product>, IProductsRepository
    {
        private RestaurantContext _context;

        public ProductsRepository(RestaurantContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProducts(int restaurantId)
        {
            return await _context.Products.Where(p => p.RestaurantId == restaurantId).Include(o => o.Category).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsByCategoryName(int restaurantId, int categoryId)
        {
            return await _context.Products.Where(p => p.RestaurantId == restaurantId && p.CategoryId == categoryId).Include(p => p.Category).ToListAsync();
        }

        public async Task<bool> ProductCorrect(Product product, Category category)
        {
            if (category == null)
                return false;

            product.CategoryId = category.Id;

            return await _context.Set<Restaurant>().AnyAsync(r => r.Id == product.RestaurantId)
                && await _context.Set<Category>().AnyAsync(c => c.Id == product.CategoryId)
                && product.Price >= 0;
        }
    }
}
