using System.Collections.Generic;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace KPZ_Restaurant_REST_API.Services
{
    public interface IStatisticsService
    {
        Task<IEnumerable<IncomeByMonth>> GetIncomeFromPast6Months(int restaurantId);
        Task<IEnumerable<ProductStatistics>> GetTop5SellingProducts(int restaurantId);
        Task<IEnumerable<ProductStatistics>> GetTop5SellingProductsFromTimePeriod(string startDate, string endDate, int restaurantId);
        Task<IEnumerable<ProductStatistics>> GetWorst5SellingProducts(int restaurantId);
        Task<IEnumerable<ProductStatistics>> GetWorst5SellingProductsFromTimePeriod(string startDate, string endDate, int restaurantId);
        Task<IEnumerable<ProductStatistics>> GetAmountOfSoldProductsByCategory(int restaurantId);
        Task<IEnumerable<ProductStatistics>> GetAmountOfSoldProductsByCategoryFromTimePeriod(string startDate, string endDate, int restaurantId);
        Task<IEnumerable<CustomerTraffic>> GetCustomerTraffic(int restaurantId, string period, int startTime, int endTime);
    }
}