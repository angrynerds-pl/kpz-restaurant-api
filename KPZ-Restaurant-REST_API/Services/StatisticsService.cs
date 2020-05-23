using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;
using KPZ_Restaurant_REST_API.Repositories;

namespace KPZ_Restaurant_REST_API.Services
{
    public class StatisticsService : IStatisticsService
    {
        private IOrdersRepository _ordersRepo;

        public StatisticsService(IOrdersRepository ordersRepo)
        {
            _ordersRepo = ordersRepo;
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
    }
}