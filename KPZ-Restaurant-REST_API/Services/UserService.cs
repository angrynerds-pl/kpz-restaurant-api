using KPZ_Restaurant_REST_API.Models;
using KPZ_Restaurant_REST_API.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace KPZ_Restaurant_REST_API.Services
{
    public class UserService : IUserService
    {
        private IUsersRepository _userRepo;

        private static int nextRestaurantId = 0;

        private String CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Rights.ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes("ABCDABCDEFGHEFGH"));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public UserService(IUsersRepository userRepo)//, IRestaurantGeneric<User> genericRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<string> AuthenticateUser(LoginModel model)
        {
            var users = await _userRepo.GetAllFiltered(user => user.Username == model.Username);
            if (users.Count != 1)
                return null;
            var user = users[0];
            if (user.Password == model.Password)
                return CreateToken(user);
            return null;
        }

        public async Task<User> AddNewWaiter(User newWaiter)
        {
            var waiterAlreadyRegistered = await _userRepo.CheckIfPresent(newWaiter);

            if (!waiterAlreadyRegistered)
            {
                await _userRepo.Add(newWaiter);
                await _userRepo.SaveAsync();
                return newWaiter;
            }
            else
                return null;
        }

        public async Task<User> AddNewManager(User manager)
        {
            
            var alreadyRegistered = await _userRepo.CheckIfPresent(manager);
            if (alreadyRegistered == true || manager.Rights != UserType.MANAGER)
                return null;
            await _userRepo.Add(manager);
            await _userRepo.SaveAsync();
            return manager;
        }

        public async Task<IEnumerable<User>> GetAllWaiters()
        {
            var waiters = await _userRepo.GetAllByRights(UserType.WAITER); //I assumed waiters are marked as 1. TODO Implement an enum to handle user rights
            var headWaiter = await _userRepo.GetAllByRights(UserType.HEAD_WAITER);
            waiters.AddRange(headWaiter);

            return waiters;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = await _userRepo.GetAll();
            return users;
        }

        public async Task<User> GetById(int id)
        {
            return await _userRepo.GetById(id);
        }
    }
}
