using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;
using KPZ_Restaurant_REST_API.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace KPZ_Restaurant_REST_API.Services
{
    public class SecurityService : ISecurityService
    {

        private IUsersRepository _userRepo;

        public SecurityService(IUsersRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Rights.ToString()),
                new Claim("Restaurant", user.RestaurantId.ToString())
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

        public async Task<string> AuthenticateUser(LoginModel model)
        {
            var user = await _userRepo.GetByUsername(model.Username);
            if (user != null && PasswordHasher.ComparePassword(model.Password, user.Password))
                return CreateToken(user);
            else
                return null;
        }

        public bool CheckIfInRole(string requiredRole, ClaimsPrincipal userPrincipal)
        {
            var role = userPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

            if (role != requiredRole)
                return false;
            else
                return true;
        }

        public int GetRestaurantId(ClaimsPrincipal userPrincipal)
        {
            int restaurantId = -1;
            string principalRestaurantId = "";
            try
            {
                principalRestaurantId = userPrincipal.Claims.FirstOrDefault(c => c.Type == "Restaurant").Value;
                restaurantId = int.Parse(principalRestaurantId);
            }
            catch (IOException e)
            {
                Console.WriteLine("Wrong restaurantId format. Should be int, was " + principalRestaurantId.GetType().ToString());
                Console.WriteLine(e.Message);
            }

            return restaurantId;

        }

    }
}