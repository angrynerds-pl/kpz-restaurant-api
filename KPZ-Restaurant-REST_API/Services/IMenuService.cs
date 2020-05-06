using System.Collections.Generic;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace KPZ_Restaurant_REST_API.Services
{
    public interface IMenuService
    {
         Task<Product> CreateNewProduct(Product product, string categoryName, int restaurantId);
         Task<IEnumerable<Product>> GetAllProducts(int restaurantId);
         Task<IEnumerable<Product>> GetProductsByCategoryName(int restaurantId, string categoryName);

         Task<Category> CreateNewCategory(Category category, int restaurantId);
         Task<IEnumerable<Category>> GetAllCategories(int restaurantId);
    }
}