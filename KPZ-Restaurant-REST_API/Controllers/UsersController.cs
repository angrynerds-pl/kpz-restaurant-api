using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;
using KPZ_Restaurant_REST_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KPZ_Restaurant_REST_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private ISecurityService _securityService;

        public UsersController(IUserService userService, ISecurityService securityService)
        {
            _userService = userService;
            _securityService = securityService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            if (!_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);

            var waiters = await _userService.GetAllUsers(restaurantId);

            return Ok(waiters);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (!_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            var user = await _userService.GetById(id, restaurantId);

            if (user != null)
                return Ok(user);
            else
                return NotFound(user);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            if (!_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            var deletedUser = await _userService.DeleteUserById(id, restaurantId);

            if (deletedUser != null)
                return Ok(deletedUser);
            else
                return NotFound(deletedUser);
        }

        [HttpPut("{userId}")]
        [Authorize]
        public async Task<ActionResult<User>> UpdateUserInfo([FromBody] User user, int userId)
        {
            if (!_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            user.Id = userId;
            var updatedUser = await _userService.UpdateUserInfo(user, restaurantId);

            if (updatedUser != null)
                return Ok(updatedUser);
            else
                return NotFound(updatedUser);
        }


        [HttpGet("waiters")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetAllWaiters()
        {
            if (!_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);

            var waiters = await _userService.GetAllWaiters(restaurantId);

            return Ok(waiters);
        }

        [HttpPost("waiters")]
        [Authorize]
        public async Task<IActionResult> AddNewWaiter([FromBody] User user)
        {
            if (!_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            user.Password = PasswordHasher.HashPassword(user.Password);

            var addedUser = await _userService.AddNewWaiter(user);

            if (addedUser != null)
                return Ok(addedUser);
            else
                return Conflict(addedUser);
        }

        [HttpGet("cooks")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetAllCooks()
        {
            if (!_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);

            var waiters = await _userService.GetAllCooks(restaurantId);

            return Ok(waiters);
        }

        [HttpPost("cooks")]
        [Authorize]
        public async Task<IActionResult> AddNewCook([FromBody] User user)
        {
            if (!_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            user.Password = PasswordHasher.HashPassword(user.Password);

            var addedUser = await _userService.AddNewCook(user);

            if (addedUser != null)
                return Ok(addedUser);
            else
                return Conflict(addedUser);
        }


    }
}