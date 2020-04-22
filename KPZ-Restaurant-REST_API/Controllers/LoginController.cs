using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;
using KPZ_Restaurant_REST_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration;

namespace KPZ_Restaurant_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private UserService _userService;
        private RestaurantService _restaurantService;

        public LoginController(UserService service, RestaurantService restaurantService)
        {
            _userService = service;
            _restaurantService = restaurantService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var newManager = new User();
            newManager.Username = model.Username;
            newManager.Rights = UserType.MANAGER;
            newManager.FirstName = model.FirstName;
            newManager.LastName = model.LastName;
            newManager.Password = model.Password;
            newManager = await _userService.AddNewManager(newManager);
            if (newManager == null)
                return BadRequest(model);
            var restaurant = new Restaurant();
            restaurant.Name = model.RestaurantName;
            restaurant = await _restaurantService.AddRestaurant(restaurant);
            newManager.RestaurantId = restaurant.Id;
            return Ok(model);
        }

        [HttpGet("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginModel model)
        {
            var userToken = await _userService.AuthenticateUser(model);
            if (userToken == null)
                return BadRequest(model);
            return Ok(userToken);
        }

        [HttpGet("user_data")]
        [Authorize]
        public async Task<IActionResult> GetUserData()
        {
            var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            var user = await _userService.GetByUsername(username);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

    }
}