using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;
using KPZ_Restaurant_REST_API.Repositories;

namespace KPZ_Restaurant_REST_API.Services
{
    public class StatisticsService : IStatisticsService
    {
        private IIncomeByMonthRepository _incomeByMonthRepo;

        public StatisticsService(IIncomeByMonthRepository incomeByMonthRepo)
        {
            _incomeByMonthRepo = incomeByMonthRepo;
        }

        public async Task<IEnumerable<IncomeByMonth>> GetIncomeFromPast6Months(int restaurantId)
        {
            var incomeFromPast6Months = new List<IncomeByMonth>();
            var pastSixMonths = new List<string>();
            for (var i = 0; i < 6; i++)
                pastSixMonths.Add(DateTime.Now.AddMonths(-i).Month.ToString());

            foreach (var month in pastSixMonths)
            {
                var incomeFromMonth = await _incomeByMonthRepo.GetIncomeFromMonth(restaurantId, month);

                if (incomeFromMonth != null)
                    incomeFromPast6Months.Add(incomeFromMonth);
            }

            return incomeFromPast6Months;
        }
    }
}