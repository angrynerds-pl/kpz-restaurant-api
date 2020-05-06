﻿using KPZ_Restaurant_REST_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Repositories
{
    public interface IProductsRepository : IRestaurantGeneric<Product>
    {
        Task<IEnumerable<Product>> GetAllProducts(int restaurantId);
        Task<bool> ProductCorrect(Product product, Category category);
    }
}
