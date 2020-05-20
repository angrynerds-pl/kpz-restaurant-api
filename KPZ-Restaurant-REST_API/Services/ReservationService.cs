using KPZ_Restaurant_REST_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Repositories;

namespace KPZ_Restaurant_REST_API.Services
{
    public class ReservationService : IReservationService
    {
        private IReservationsRepository _reservationsRepo;

        public ReservationService(IReservationsRepository reservationRepo)
        {
            _reservationsRepo = reservationRepo;
        }
        public async Task<Reservation> CreateNewReservation(Reservation newReservation, int restaurantID)
        {
            newReservation.RestaurantId = restaurantID;
            await _reservationsRepo.Add(newReservation);
            await _reservationsRepo.SaveAsync();
            return newReservation;
        }

        public async Task<Reservation> DeleteReservation(int reservationID, int restaurantID)
        {
            var reservation = await _reservationsRepo.DeleteReservationById(reservationID, restaurantID);
            await _reservationsRepo.SaveAsync();
            return reservation;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByDate(int year, int month, int day, int hours, int minutes, int restaurantId)
        {
            DateTime date = new DateTime(year, month, day, hours, minutes, 0);
            var reservations = await _reservationsRepo.GetWhere(reservation =>
                reservation.StartDate <= date && reservation.EndDate >= date && reservation.RestaurantId == restaurantId);
            return reservations;
        }

        public async Task<IEnumerable<Reservation>> GetTodayReservations(int restaurantId)
        {
            var today = DateTime.Today;
            var reservations = await _reservationsRepo.GetWhere(reservation => reservation.StartDate.Date == today && reservation.RestaurantId == restaurantId);
            return reservations;
        }

        public async Task<Reservation> UpdateReservation(Reservation res, int restaurantId)
        {
            res.RestaurantId = restaurantId;
            var reservation = await _reservationsRepo.UpdateReservation(res);
            if (reservation != null)
            {
                return reservation;
            }
            return null;
        }


    }
}
