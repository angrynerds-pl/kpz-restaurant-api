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
    [Route("/api/[controller]")]
    [ApiController]
    public class CooksController : ControllerBase
    {
        private IUserService _userService;

        public CooksController(IUserService userService)
        {
            _userService = userService;
        }

        private bool CheckIfInRole(string requiredRole)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

            if (role != requiredRole)
                return false;
            else
                return true;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetAllCooks()
        {
            if (!CheckIfInRole("ADMIN"))
                return Unauthorized();

            var cooks = await _userService.GetAllCooks();

            return Ok(cooks);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> GetCook(int id)
        {
            if (!CheckIfInRole("ADMIN"))
                return Unauthorized();

            var user = await _userService.GetById(id);

            if (user != null)
                return Ok(user);
            else
                return NotFound(user);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddNewCook([FromBody] User user)
        {
            if (!CheckIfInRole("ADMIN"))
                return Unauthorized();

            var addedUser = await _userService.AddNewCook(user);

            if (addedUser != null)
                return Ok(addedUser);
            else
                return Conflict(addedUser);
        }

    }
}