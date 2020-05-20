using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ReservationsController : ControllerBase
    {

        private IReservationService _reservationService;
        private ISecurityService _securityService;

        public ReservationsController(IReservationService reservationService, ISecurityService securityService)
        {
            _reservationService = reservationService;
            _securityService = securityService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Reservation>> CreateNewReservation([FromBody] Reservation reservation)
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User) && !_securityService.CheckIfInRole("WAITER", User) && !_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();
            var restaurantId = _securityService.GetRestaurantId(User);
            var createdReservation = await  _reservationService.CreateNewReservation(reservation, restaurantId);
            if (createdReservation == null)
                return BadRequest(createdReservation);
            return Ok(createdReservation);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetTodayReservations()
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User) && !_securityService.CheckIfInRole("WAITER", User))
                return Unauthorized();
            var restaurantId = _securityService.GetRestaurantId(User);
            var reservations = await _reservationService.GetTodayReservations(restaurantId);
            if (reservations == null)
                return BadRequest(reservations);
            return Ok(reservations);
        }

        [HttpGet("{year}/{month}/{day}/{hours}/{minutes}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservationsByDate(int year, int month, int day, int hours, int minutes)
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User) && !_securityService.CheckIfInRole("WAITER", User) && !_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();
            var restaurantId = _securityService.GetRestaurantId(User);
            var reservations =
                await _reservationService.GetReservationsByDate(year, month, day, hours, minutes, restaurantId);
            if (reservations == null)
                return BadRequest(reservations); 
            return Ok(reservations);
        }

        [HttpDelete("{reservationId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Reservation>>> RemoveReservation(int reservationId)
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User) && !_securityService.CheckIfInRole("WAITER", User) && !_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();
            var restaurantId = _securityService.GetRestaurantId(User);
            var reservation = await _reservationService.DeleteReservation(reservationId, restaurantId);
            if (reservation == null)
                return BadRequest(reservation);
            return Ok(reservation);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<Reservation>> UpdateReservation([FromBody]Reservation reservation)
        {
            if (!_securityService.CheckIfInRole("HEAD_WAITER", User)  && !_securityService.CheckIfInRole("MANAGER", User))
                return Unauthorized();
            var restaurantId = _securityService.GetRestaurantId(User);
            var _reservation = _reservationService.UpdateReservation(reservation, restaurantId);
            if (_reservation != null)
                return Ok(_reservation);
            return BadRequest(_reservation);
        }
    }
}