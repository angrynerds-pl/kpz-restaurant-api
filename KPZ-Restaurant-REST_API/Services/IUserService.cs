using KPZ_Restaurant_REST_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Services
{
    public interface IUserService
    {
        Task<User> AddNewWaiter(User newWaiter);

        Task<User> AddNewManager(RegisterModel manager, int restaurantId);

        Task<User> GetByUsername(string username);
        Task<IEnumerable<User>> GetAllUsers(int restaurantId);
        Task<IEnumerable<User>> GetAllWaiters(int restaurantId);
        Task<User> GetById(int id);
        Task<IEnumerable<User>> GetAllCooks(int restaurantId);
        Task<User> AddNewCook(User user);
    }
}
