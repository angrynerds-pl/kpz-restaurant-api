using System.Security.Claims;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;
using KPZ_Restaurant_REST_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KPZ_Restaurant_REST_API.Services
{
    public interface ISecurityService
    {
        string CreateToken(User user);
        Task<string> AuthenticateUser(LoginModel model);
        bool CheckIfInRole(string requiredRole, ClaimsPrincipal userPrincipal);
        int GetRestaurantId(ClaimsPrincipal userPrincipal);
    }
}