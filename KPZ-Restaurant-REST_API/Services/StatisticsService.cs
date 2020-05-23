using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;
using KPZ_Restaurant_REST_API.Repositories;

namespace KPZ_Restaurant_REST_API.Services
{
    public class StatisticsService : IStatisticsService
    {
        private IOrdersRepository _ordersRepo;
        private IOrderedProductsRepository _orderedProductsRepo;
        private ICategoriesRepository _categoriesRepo;
        private IProductsRepository _productsRepo;

        public StatisticsService(IOrdersRepository ordersRepo, IOrderedProductsRepository orderedProductsRepo, ICategoriesRepository categoriesRepo, IProductsRepository productsRepo)
        {
            _ordersRepo = ordersRepo;
            _orderedProductsRepo = orderedProductsRepo;
            _categoriesRepo = categoriesRepo;
            _productsRepo = productsRepo;
        }

        public async Task<IEnumerable<ProductStatistics>> GetAmountOfSoldProductsByCategory(int restaurantId)
        {
            var soldProducts = new List<ProductStatistics>();
            var categories = await _categoriesRepo.GetAllCategories(restaurantId);

            foreach (var category in categories)
            {
                var productQuant = new ProductStatistics() { Name = category.Name, Quantity = await _orderedProductsRepo.GetAmountOfSoldProductsByCategory(restaurantId, category.Name) };
                soldProducts.Add(productQuant);
            }

            return soldProducts;
        }

        public async Task<IEnumerable<ProductStatistics>> GetTop5SellingProducts(int restaurantId)
        {
            var topSellingProducts = new List<ProductStatistics>();
            var productNames = await _productsRepo.GetAllProducts(restaurantId);

            foreach (var product in productNames)
            {
                var productQuant = new ProductStatistics() { Name = product.Name, Quantity = await _orderedProductsRepo.GetSoldProductCount(restaurantId, product.Name) };
                topSellingProducts.Add(productQuant);
            }

            return topSellingProducts.OrderByDescending(p => p.Quantity).Take(5).ToList();
        }

        public async Task<IEnumerable<IncomeByMonth>> GetIncomeFromPast6Months(int restaurantId)
        {
            var incomeFromPast6Months = new List<IncomeByMonth>();
            var pastSixMonths = new List<int>();
            for (var i = 0; i < 6; i++)
                pastSixMonths.Add(DateTime.Now.AddMonths(-i).Month);

            foreach (var month in pastSixMonths)
            {
                var firstDayOfMonth = new DateTime(DateTime.Now.Year, month, 1);
                var monthRange = new DateRange() { StartDate = firstDayOfMonth, EndDate = firstDayOfMonth.AddMonths(1) };
                var ordersFromMonth = await _ordersRepo.GetOrdersByDateRange(monthRange, restaurantId);

                var income = new IncomeByMonth() { Month = firstDayOfMonth.ToString("MMMM", CultureInfo.InvariantCulture), Income = 0 };
                foreach (var order in ordersFromMonth)
                {
                    if (order.OrderedProducts != null && order.Status == "PAID")
                    {
                        foreach (var product in order.OrderedProducts)
                            income.Income += product.Product.Price;
                    }
                }

                incomeFromPast6Months.Add(income);
            }

            return incomeFromPast6Months;
        }


        public async Task<IEnumerable<ProductStatistics>> GetWorst5SellingProducts(int restaurantId)
        {
            var topSellingProducts = new List<ProductStatistics>();
            var productNames = await _productsRepo.GetAllProducts(restaurantId);

            foreach (var product in productNames)
            {
                var productQuant = new ProductStatistics() { Name = product.Name, Quantity = await _orderedProductsRepo.GetSoldProductCount(restaurantId, product.Name) };
                topSellingProducts.Add(productQuant);
            }

            return topSellingProducts.OrderBy(p => p.Quantity).Take(5).ToList();
        }

        public async Task<IEnumerable<CustomerTraffic>> GetCustomerTraffic(int restaurantId, string period, int startTime, int endTime)
        {
            var customerTraffic = new List<CustomerTraffic>();
            if (startTime > endTime || startTime + 2 > endTime)
                return null;

            var startTimeSpan = new TimeSpan(startTime, 0, 0);
            var endTimeSpan = new TimeSpan(endTime, 0, 0);

            int periodLength = 2;
            int numberOfOrders = 0;
            while (startTimeSpan < endTimeSpan)
            {
                if (startTimeSpan.Add(TimeSpan.FromHours(periodLength)) > endTimeSpan)
                    periodLength = 1;

                if (period == "TODAY")
                    numberOfOrders = await _ordersRepo.GetNumberOfOrdersFromToday(restaurantId, startTimeSpan, startTimeSpan.Add(TimeSpan.FromHours(periodLength)));
                else if (period == "WEEK")
                    numberOfOrders = await _ordersRepo.GetNumberOfOrdersFromWeek(restaurantId, startTimeSpan, startTimeSpan.Add(TimeSpan.FromHours(periodLength)));
                else if (period == "MONTH")
                    numberOfOrders = await _ordersRepo.GetNumberOfOrdersFromMonth(restaurantId, startTimeSpan, startTimeSpan.Add(TimeSpan.FromHours(periodLength)));


                customerTraffic.Add(new CustomerTraffic() { StartTime = startTimeSpan, EndTime = startTimeSpan.Add(TimeSpan.FromHours(periodLength)), Quantity = numberOfOrders });

                if (periodLength != 1)
                    startTimeSpan = startTimeSpan.Add(TimeSpan.FromHours(periodLength));
                else
                    startTimeSpan = endTimeSpan;
            }

            return customerTraffic;
        }
    }
}