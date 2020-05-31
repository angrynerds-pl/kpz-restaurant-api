using KPZ_Restaurant_REST_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Repositories
{
    public class CategoriesRepository : RestaurantGeneric<Category>, ICategoriesRepository
    {
        private RestaurantContext _context;

        public CategoriesRepository(RestaurantContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CategoryCorrect(Category category)
        {
            return (! await _context.Categories.AnyAsync(c => c.Name == category.Name && c.RestaurantId == category.RestaurantId)) 
            && ( await _context.Restaurants.AnyAsync(r => r.Id == category.RestaurantId));
        }

        public async Task<IEnumerable<Category>> GetAllCategories(int restaurantId)
        {
            return await _context.Categories.Where(c => c.RestaurantId == restaurantId ).ToListAsync();
        }

        public async Task<Category> GetCategoryByName(string categoryName)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Name == categoryName );
        }
    }
}
