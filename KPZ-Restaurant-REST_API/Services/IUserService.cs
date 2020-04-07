using KPZ_Restaurant_REST_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Services
{
    public interface IUserService
    {
        User AddNewWaiter(User newWaiter);
        IEnumerable<User> GetAllWaiters();
    }
}
