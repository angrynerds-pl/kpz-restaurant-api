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
    public class UserController: ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        private bool CheckIfInRole(string requiredRole)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

            if (role != requiredRole)
                return false;
            return true;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            if (!CheckIfInRole("MANAGER"))
                return Unauthorized();

            var waiters = await _userService.GetAllUsers();

            return Ok(waiters);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (!CheckIfInRole("MANAGER"))
                return Unauthorized();

            var user = await _userService.GetById(id);
            
            if (user != null)
                return Ok(user);
            else
                return NotFound(user);
        }

        [HttpGet("waiters")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetAllWaiters()
        {
            if (!CheckIfInRole("MANAGER"))
                return Unauthorized();

            var waiters = await _userService.GetAllWaiters();

            return Ok(waiters);
        }

        [HttpPost("waiters")]
        [Authorize]
        public async Task<IActionResult> AddNewWaiter([FromBody] User user)
        {
            if (!CheckIfInRole("MANAGER"))
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
            if (!CheckIfInRole("MANAGER"))
                return Unauthorized();

            var waiters = await _userService.GetAllCooks();

            return Ok(waiters);
        }

        [HttpPost("cooks")]
        [Authorize]
        public async Task<IActionResult> AddNewCook([FromBody] User user)
        {
            if (!CheckIfInRole("MANAGER"))
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