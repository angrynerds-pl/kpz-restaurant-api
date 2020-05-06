using System.Collections.Generic;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;
using KPZ_Restaurant_REST_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KPZ_Restaurant_REST_API.Services
{
    public class MenuService : IMenuService
    {
        private IProductsRepository _productsRepo;
        private ICategoriesRepository _categoriesRepo;

        public MenuService(IProductsRepository productsRepo, ICategoriesRepository categoriesRepo)
        {
            _productsRepo = productsRepo;
            _categoriesRepo = categoriesRepo;
        }

        public async Task<ActionResult<Category>> CreateNewCategory(Category category, int restaurantId)
        {
            category.RestaurantId = restaurantId;
            var categoryCorrect = await _categoriesRepo.CategoryCorrect(category);

            if (categoryCorrect)
            {
                await _categoriesRepo.Add(category);
                await _categoriesRepo.SaveAsync();
                return category;
            }
            else
                return null;

        }

        public async Task<ActionResult<Product>> CreateNewProduct(Product product, int restaurantId)
        {
            product.RestaurantId = restaurantId;
            var productCorrect = await _productsRepo.ProductCorrect(product);

            if (productCorrect)
            {
                await _productsRepo.Add(product);
                await _productsRepo.SaveAsync();
                return product;
            }
            else
                return null;
        }

        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories(int restaurantId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts(int restaurantId)
        {
            throw new System.NotImplementedException();
        }
    }
}