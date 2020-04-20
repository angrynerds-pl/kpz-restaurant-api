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
        Task<IEnumerable<User>> GetAllWaiters();
        Task<User> GetById(int id);
    }
}
