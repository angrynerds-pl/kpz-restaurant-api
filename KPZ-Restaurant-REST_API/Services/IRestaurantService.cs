using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;

namespace KPZ_Restaurant_REST_API.Services
{
    public interface IRestaurantService
    {
        Task<Restaurant> AddRestaurant(RegisterModel registerModel);
    }
}