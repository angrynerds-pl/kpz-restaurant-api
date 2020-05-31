using KPZ_Restaurant_REST_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Repositories
{
    public class RoomsRepository : RestaurantGeneric<Room>, IRoomsRepository
    {
        private RestaurantContext _context;

        public RoomsRepository(RestaurantContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Room> DeleteRoomById(int roomId, int restaurantId)
        {
            var roomToDelete = await _context.Rooms.Where(r => r.Id == roomId && r.RestaurantId == restaurantId ).FirstOrDefaultAsync();
            if (roomToDelete != null)
            {
                roomToDelete.DeletedAt = DateTime.Now;
                _context.Rooms.Update(roomToDelete);
                return roomToDelete;
            }
            else
                return null;
        }

        public async Task<IEnumerable<Room>> GetAllRooms(int restaurantId)
        {
            return await _context.Rooms.Where(r => r.RestaurantId == restaurantId ).ToListAsync();
        }

        public async Task<bool> RoomCorrect(Room room)
        {
            return await _context.Set<Restaurant>().AnyAsync(r => r.Id == room.RestaurantId)
                && !(await _context.Rooms.AnyAsync(r => r.Name == room.Name && r.RestaurantId == room.RestaurantId));
        }

    }

}
