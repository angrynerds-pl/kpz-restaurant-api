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
        private ISecurityService _securityService;

        public RoomsController(IRoomService roomService, ISecurityService securityService)
        {
            _roomService = roomService;
            _securityService = securityService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Room>>> GetAllRooms()
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User) && !_securityService.CheckIfInRole("WAITER", User) && !_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);

            var rooms = await _roomService.GetAllRooms(restaurantId);
            return Ok(rooms);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Room>> CreateNewRoom([FromBody] Room room)
        {
            if (!_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var createdRoom = await _roomService.CreateNewRoom(room);

            if (createdRoom != null)
                return Ok(createdRoom);
            else
                return BadRequest(createdRoom);
        }

        [HttpDelete("{roomId}")]
        [Authorize]
        public async Task<ActionResult<Room>> DeleteRoomById(int roomId)
        {
            if (!_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();

            var restaurantId = _securityService.GetRestaurantId(User);
            var removedRoom = await _roomService.DeleteRoomById(roomId, restaurantId);

            if (removedRoom != null)
                return Ok(removedRoom);
            else
                return NotFound(removedRoom);
        }

    }
}
