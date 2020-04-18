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
                if (newWaiter.Rights == UserType.WAITER || newWaiter.Rights == UserType.HEAD_WAITER)
                {
                    _userRepo.Create(newWaiter);
                    _userRepo.Save();
                    return newWaiter;
                }
            }
            return null;
        }

        public IEnumerable<User> GetAllWaiters()
        {
            return _userRepo.GetAllByRights(UserType.WAITER); //I assumed waiters are marked as 1. TODO Implement an enum to handle user rights
        }
    }
}
