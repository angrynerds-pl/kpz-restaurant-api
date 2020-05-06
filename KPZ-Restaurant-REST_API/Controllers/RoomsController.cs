using System;
using System.Collections.Generic;
using System.IO;
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

        private bool CheckIfInRole(string requiredRole)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

            if (role != requiredRole)
                return false;
            else
                return true;
        }

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetAllRooms()
        {
            int restaurantId;
            restaurantId = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "Restaurant").Value);

            var rooms = await _roomService.GetAllRooms(restaurantId);
            return Ok(rooms);
        }

        [HttpPost]
        public async Task<ActionResult<Room>> CreateNewRoom([FromBody] Room room)
        {
            var createdRoom = await _roomService.CreateNewRoom(room);

            if (createdRoom != null)
                return Ok(createdRoom);
            else
                return BadRequest(createdRoom);
        }

        [HttpDelete("{roomId}")]
        public async Task<ActionResult<Room>> DeleteRoomById(int roomId)
        {
            var removedRoom = await _roomService.DeleteRoomById(roomId);

            if (removedRoom != null)
                return Ok(removedRoom);
            else
                return NotFound(removedRoom);
        }

    }
}
