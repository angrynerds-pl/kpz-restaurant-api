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

        public UserService(IUsersRepository userRepo)//, IRestaurantGeneric<User> genericRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<User> AddNewWaiter(User newWaiter)
        {
            var waiterAlreadyRegistered = await _userRepo.CheckIfPresent(newWaiter);

            if (!waiterAlreadyRegistered)
            {
                _userRepo.Create(newWaiter);
                _userRepo.SaveAsync();
                return newWaiter;
            }
            else
                return null;
        }

        public async Task<IEnumerable<User>> GetAllWaiters()
        {
            var waiters = await _userRepo.GetAllByRights(UserType.WAITER); //I assumed waiters are marked as 1. TODO Implement an enum to handle user rights
            var headWaiter = await _userRepo.GetAllByRights(UserType.HEAD_WAITER);
            waiters.AddRange(headWaiter);

            return waiters;
        }

        public async Task<User> GetById(int id)
        {
            return await _userRepo.GetById(id);
        }
    }
}
