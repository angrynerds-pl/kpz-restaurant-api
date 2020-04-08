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
    public class WaiterController : ControllerBase
    {
        private IUserService _userService;

        public WaiterController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllWaiters()
        {
            var waiters = _userService.GetAllWaiters();

            return Ok(waiters);
        }

        [HttpPost]
        public IActionResult AddNewWaiter([FromBody] User user)
        {
            var addedUser = _userService.AddNewWaiter(user);
            if(addedUser != null)
            {
                return Ok(addedUser);
            }
            else
            {
                return Conflict(addedUser);
            }
        }
    }
}