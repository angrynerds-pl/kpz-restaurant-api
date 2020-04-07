using KPZ_Restaurant_REST_API.Models;
using KPZ_Restaurant_REST_API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Services
{
    public class UserService : IUserService
    {
        private IUsersRepository _userRepo;
        //private IRestaurantGeneric<User> _genericRepo;

        public UserService(IUsersRepository userRepo)//, IRestaurantGeneric<User> genericRepo)
        {
            _userRepo = userRepo;
            //_genericRepo = genericRepo;
        }

        public User AddNewWaiter(User newWaiter)
        {
            if (!_userRepo.CheckIfPresent(newWaiter))
            {
                _userRepo.Create(newWaiter);
                return newWaiter;
            }
            else
                return null;
        }

        public IEnumerable<User> GetAllWaiters()
        {
            //TODO: Implement return of all WAITERS not USERS
            return _userRepo.GetAll();
        }
    }
}
