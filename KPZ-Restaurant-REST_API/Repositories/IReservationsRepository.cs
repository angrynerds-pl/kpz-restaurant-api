using KPZ_Restaurant_REST_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Repositories
{
    public interface IReservationsRepository : IRestaurantGeneric<Reservation>
    {
        Task<Reservation> DeleteReservationById(int id, int restaurantId);

        Task<Reservation> UpdateReservation(Reservation reservation);
    }
}
