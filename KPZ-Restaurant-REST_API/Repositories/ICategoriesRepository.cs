﻿using KPZ_Restaurant_REST_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Repositories
{
    public interface ICategoriesRepository: IRestaurantGeneric<Category>
    {
        Task<bool> CategoryCorrect(Category category);
        Task<IEnumerable<Category>> GetAllCategories(int restaurantId); 
        Task<Category> GetCategoryByName(string categoryName);
    }
}
