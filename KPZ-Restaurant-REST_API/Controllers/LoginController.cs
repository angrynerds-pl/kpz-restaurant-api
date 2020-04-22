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
        private readonly IUserService _userService;
        private readonly IRestaurantService _restaurantService;

        public LoginController(IUserService service, IRestaurantService restaurantService)
        {
            _userService = service;
            _restaurantService = restaurantService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var restaurant = await _restaurantService.AddRestaurant(model);
            var newManager = await _userService.AddNewManager(model, restaurant.Id);

            if (newManager == null)
                return BadRequest(model);
            else
            {
                newManager.RestaurantId = restaurant.Id;
                return Ok(model);
            }
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginModel model)
        {
            var userToken = await _userService.AuthenticateUser(model);
            if (userToken == null)
                return BadRequest(model);
            return Ok( new { token = userToken });
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