using KPZ_Restaurant_REST_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Repositories
{
    public class ReservationsRepository : RestaurantGeneric<Reservation>, IReservationsRepository
    {
        RestaurantContext _context;

        public ReservationsRepository(RestaurantContext context) : base(context)
        {
            _context = context;
        }
    }
}
