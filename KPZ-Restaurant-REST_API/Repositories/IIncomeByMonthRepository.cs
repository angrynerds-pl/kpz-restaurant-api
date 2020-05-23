using System.Collections.Generic;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;

namespace KPZ_Restaurant_REST_API.Repositories
{
    public interface IIncomeByMonthRepository : IRestaurantGeneric<IncomeByMonth>
    {
        Task IncreaseIncome(int restaurantId, decimal price, string month);
        Task<IncomeByMonth> GetIncomeFromMonth(int restaurantId, string month);
    }
}