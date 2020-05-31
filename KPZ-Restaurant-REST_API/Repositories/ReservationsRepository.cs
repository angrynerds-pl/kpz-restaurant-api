using KPZ_Restaurant_REST_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KPZ_Restaurant_REST_API.Repositories
{
    public class ReservationsRepository : RestaurantGeneric<Reservation>, IReservationsRepository
    {
        private RestaurantContext _context;

        public ReservationsRepository(RestaurantContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Reservation> DeleteReservationById(int id, int restaurantId)
        {
            var reservationToDelete = await _context.Reservations.Where(r => r.Id == id && r.RestaurantId == restaurantId && r.DeletedAt == null).FirstOrDefaultAsync();
            if (reservationToDelete != null)
            {
                reservationToDelete.DeletedAt = DateTime.Now;
                _context.Reservations.Update(reservationToDelete);
                return reservationToDelete;
            }
            return null;
        }

        public async Task<Reservation> UpdateReservation(Reservation reservation)
        {
            var entity = await _context.Reservations.FindAsync(reservation.Id);
            if (entity != null)
            {
                entity.RestaurantId = reservation.RestaurantId;
                entity.CustomerName = reservation.CustomerName;
                entity.DeletedAt = reservation.DeletedAt;
                entity.EndDate = entity.EndDate;
                entity.NumberOfSeats = reservation.NumberOfSeats;
                entity.StartDate = reservation.StartDate;
                entity.TableId = reservation.TableId;
                entity.Note = reservation.Note;
                _context.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            else
                return null;
        }
    }
}
