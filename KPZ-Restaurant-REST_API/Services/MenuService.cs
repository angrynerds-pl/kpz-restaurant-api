using System;
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

        public async Task<Category> CreateNewCategory(Category category, int restaurantId)
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

        public async Task<Product> CreateNewProduct(Product product, string categoryName, int restaurantId)
        {
            product.RestaurantId = restaurantId;
            var categoryInDatabase = await _categoriesRepo.GetCategoryByName(categoryName);
            var productCorrect = await _productsRepo.ProductCorrect(product, categoryInDatabase);

            if (productCorrect)
            {
                await _productsRepo.Add(product);
                await _productsRepo.SaveAsync();
                return product;
            }
            else
                return null;
        }

        public async Task<IEnumerable<Category>> GetAllCategories(int restaurantId)
        {
            return await _categoriesRepo.GetAllCategories(restaurantId);
        }

        public async Task<IEnumerable<Product>> GetAllProducts(int restaurantId)
        {
            return await _productsRepo.GetAllProducts(restaurantId);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryName(int restaurantId, string categoryName)
        {
            var categoryInDatabse = await _categoriesRepo.GetCategoryByName(categoryName);
            if(categoryInDatabse == null)
                return null;

            return await _productsRepo.GetAllProductsByCategoryName(restaurantId, categoryInDatabse.Id);
        }
    }
}