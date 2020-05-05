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
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {

        private IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("{restaurantId}")]
        public async Task<ActionResult<IEnumerable<Room>>> GetAllRooms(int restaurantId)
        {
            var rooms = await _roomService.GetAllRooms(restaurantId);
            return Ok(rooms);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewRoom([FromBody] Room room)
        {
            var createdRoom = await _roomService.CreateNewRoom(room);

            if (createdRoom != null)
                return Ok(createdRoom);
            else
                return BadRequest(createdRoom);
        }

    }
}
