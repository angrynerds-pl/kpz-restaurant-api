﻿using KPZ_Restaurant_REST_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Repositories
{
    public interface IOrderedProductsRepository : IRestaurantGeneric<OrderedProducts>
    {
        Task<bool> OrderedProductCorrect(OrderedProducts orderedProduct, int restaurantId);
        Task<IList<OrderedProducts>> GetOrderedProducts(int orderId, int restaurantId); 
        Task<OrderedProducts> UpdateOrderedProduct(OrderedProducts orderedProduct);
        Task<OrderedProducts> DeleteOrderedProductById(int orderedProductId, int restaurantId);
        Task<OrderedProducts> GetOrderedProductById(int id, int restaurantId);
        Task<IList<OrderedProducts>> GetServedProducts(int orderId, int restaurantId);
        Task<int> GetAmountOfSoldProductsByCategory(int restaurantId, string category);
        Task<int> GetAmountOfSoldProductsByCategory(int restaurantId, string category, DateTime startDate, DateTime endDate);
        Task<int> GetSoldProductCount(int restaurantId, string productName);
        Task<int> GetSoldProductCountFromTimePeriod(int restaurantId, string name, DateTime startDateTime, DateTime endDateTime);
    }
}
