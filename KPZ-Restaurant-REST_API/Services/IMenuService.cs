using System.Collections.Generic;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace KPZ_Restaurant_REST_API.Services
{
    public interface IMenuService
    {
         Task<ActionResult<Product>> CreateNewProduct(Product product, int restaurantId);
         Task<ActionResult<IEnumerable<Product>>> GetAllProducts(int restaurantId);
         Task<ActionResult<Category>> CreateNewCategory(Category category, int restaurantId);
         Task<ActionResult<IEnumerable<Category>>> GetAllCategories(int restaurantId);
    }
}