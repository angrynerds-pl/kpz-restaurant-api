using KPZ_Restaurant_REST_API.Models;
using KPZ_Restaurant_REST_API.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace KPZ_Restaurant_REST_API.Services
{
    public class UserService : IUserService
    {
        private IUsersRepository _userRepo;

        public UserService(IUsersRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<User> GetByUsername(string username)
        {
            var users = await _userRepo.GetByUsername(username);
            if (users.Count != 1)
                return null;
            return users.FirstOrDefault();
        }

        public async Task<User> AddNewWaiter(User newWaiter)
        {
            var waiterAlreadyRegistered = await _userRepo.CheckIfPresent(newWaiter);

            if (!waiterAlreadyRegistered && (newWaiter.Rights != UserType.HEAD_WAITER || newWaiter.Rights != UserType.WAITER))
            {
                await _userRepo.Add(newWaiter);
                await _userRepo.SaveAsync();
                return newWaiter;
            }
            else
                return null;
        }

        public async Task<User> AddNewManager(RegisterModel manager, int restaurantId)
        {
            User newManager = new User()
            {
                Username = manager.Username,
                Password = manager.Password,
                FirstName = manager.FirstName,
                LastName = manager.LastName,
                Rights = UserType.MANAGER,
                RestaurantId = restaurantId
            };

            var alreadyRegistered = await _userRepo.CheckIfPresent(newManager);
            if (alreadyRegistered == true || newManager.Rights != UserType.MANAGER)
                return null;
            await _userRepo.Add(newManager);
            await _userRepo.SaveAsync();
            return newManager;
        }

        public async Task<IEnumerable<User>> GetAllWaiters(int restaurantId)
        {
            var waiters = await _userRepo.GetAllByRights(UserType.WAITER, restaurantId);
            var headWaiter = await _userRepo.GetAllByRights(UserType.HEAD_WAITER, restaurantId);
            waiters.Concat(headWaiter);

            return waiters;
        }

        public async Task<IEnumerable<User>> GetAllUsers(int restaurantId)
        {
            return await _userRepo.GetAllUsers(restaurantId);
        }

        public async Task<User> GetById(int id)
        {
            return await _userRepo.GetUserById(id);
        }

        public async Task<IEnumerable<User>> GetAllCooks(int restaurantId)
        {
            return await _userRepo.GetAllByRights(UserType.COOK, restaurantId);
        }

        public async Task<User> AddNewCook(User user)
        {
            var cookAlreadyRegistered = await _userRepo.CheckIfPresent(user);

            if (!cookAlreadyRegistered && user.Rights == UserType.COOK)
            {
                await _userRepo.Add(user);
                await _userRepo.SaveAsync();
                return user;
            }
            else
                return null;
        }
    }
}
