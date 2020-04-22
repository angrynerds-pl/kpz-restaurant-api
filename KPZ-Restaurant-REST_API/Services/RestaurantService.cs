using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;
using KPZ_Restaurant_REST_API.Repositories;

namespace KPZ_Restaurant_REST_API.Services
{
    public class RestaurantService : IRestaurantService
    {
        private IRestaurantRepository _restaurantRepo;

        private static int nextRestaurantId = 0;

        public RestaurantService(IRestaurantRepository restaurantRepo)//, IRestaurantGeneric<User> genericRepo)
        {
            _restaurantRepo = restaurantRepo;
        }

        public async Task<Restaurant> AddRestaurant(Restaurant restaurant)
        {
            await _restaurantRepo.Add(restaurant);
            return restaurant;
        }
    }
}