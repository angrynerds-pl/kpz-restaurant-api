using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;
using KPZ_Restaurant_REST_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KPZ_Restaurant_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaitersController : ControllerBase
    {
        private IUserService _userService;

        public WaitersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllWaiters()
        {
            var waiters = await _userService.GetAllWaiters();

            return Ok(waiters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetWaiter(int id)
        {
            var user = await _userService.GetById(id);
            
            if (user != null)
                return Ok(user);
            else
                return NotFound(null);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewWaiter([FromBody] User user)
        {
            var addedUser = await _userService.AddNewWaiter(user);

            if (addedUser != null)
                return Ok(addedUser);
            else
                return Conflict(addedUser);
        }
    }
}