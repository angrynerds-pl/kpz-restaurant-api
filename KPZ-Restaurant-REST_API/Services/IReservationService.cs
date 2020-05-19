using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;

namespace KPZ_Restaurant_REST_API.Services
{
    public interface IReservationService
    {
        Task<Reservation> CreateNewReservation(Reservation newReservation, int restaurantId);
        Task<IEnumerable<Reservation>> GetReservationsByDate(int year, int month, int day, int hours, int minutes, int restaurantId);

        Task<IEnumerable<Reservation>> GetTodayReservations(int restaurantId);

        Task<Reservation> DeleteReservation(int reservationID, int restaurantId);
    }
}
