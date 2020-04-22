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

        Task<User> AddNewManager(User manager);

        Task<User> GetByUsername(string username);

        Task<string> AuthenticateUser(LoginModel model);
        Task<IEnumerable<User>> GetAllUsers();
        Task<IEnumerable<User>> GetAllWaiters();
        Task<User> GetById(int id);
    }
}
