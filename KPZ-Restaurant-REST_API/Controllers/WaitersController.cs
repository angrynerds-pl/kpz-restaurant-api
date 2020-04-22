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

namespace KPZ_Restaurant_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaitersController : ControllerBase
    {
        private IUserService _userService;

        private bool checkIfInRole(string requiredRole)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

            if (role != requiredRole)
                return false;
            return true;
        }

        public WaitersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetAllWaiters()
        {
            if (!checkIfInRole("ADMIN"))
                return Unauthorized();

            var waiters = await _userService.GetAllWaiters();

            return Ok(waiters);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> GetWaiter(int id)
        {
            if (!checkIfInRole("ADMIN"))
                return Unauthorized();

            var user = await _userService.GetById(id);
            
            if (user != null)
                return Ok(user);
            else
                return NotFound(user);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddNewWaiter([FromBody] User user)
        {
            if (!checkIfInRole("ADMIN"))
                return Unauthorized();

            var addedUser = await _userService.AddNewWaiter(user);

            if (addedUser != null)
                return Ok(addedUser);
            else
                return Conflict(addedUser);
        }
    }
}