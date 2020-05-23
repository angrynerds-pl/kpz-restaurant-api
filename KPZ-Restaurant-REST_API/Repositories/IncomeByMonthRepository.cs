using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;
using Microsoft.EntityFrameworkCore;

namespace KPZ_Restaurant_REST_API.Repositories
{
    public class IncomeByMonthRepository: RestaurantGeneric<IncomeByMonth>, IIncomeByMonthRepository
    {
        private RestaurantContext _context;

        public IncomeByMonthRepository(RestaurantContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IncomeByMonth> GetIncomeFromMonth(int restaurantId, string month)
        {
            return await _context.IncomeByMonth.Where(i => i.RestaurantId == restaurantId && i.Month == month).FirstOrDefaultAsync();
        }

        public async Task IncreaseIncome(int restaurantId, decimal price, string month)
        {
            var incomeByMonth = await _context.IncomeByMonth.FirstOrDefaultAsync(i => i.RestaurantId == restaurantId && i.Month == month);
            if(incomeByMonth != null) 
            {
                incomeByMonth.Income += price;
                _context.Update(incomeByMonth);
            }
            else
            {
                incomeByMonth = new IncomeByMonth() {RestaurantId = restaurantId, Month = month, Income = price};
                _context.Add(incomeByMonth);
            }
        }

    }
}