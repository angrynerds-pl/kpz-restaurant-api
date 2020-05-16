using KPZ_Restaurant_REST_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Repositories
{
    public class TablesRepository: RestaurantGeneric<Table>, ITablesRepository
    {
        private RestaurantContext _context;

        public TablesRepository(RestaurantContext context): base(context) 
        {
            _context = context;
        }

        public async Task<bool> CheckIfTablePresent(Table table, int restaurantId)
        {
            return await _context.Tables.AnyAsync(t => t.Number == table.Number && t.RoomId == table.RoomId && t.Room.RestaurantId == restaurantId );
        }

        public async Task<Table> DeleteTableById(int id, int restaurantId)
        {
            var tableToDelete = await _context.Tables.Where(t => t.Id == id && t.Room.RestaurantId == restaurantId ).FirstOrDefaultAsync();
            if (tableToDelete != null)
            {
                tableToDelete.DeletedAt = DateTime.Now;
                _context.Tables.Update(tableToDelete);
                return tableToDelete;
            }
            else
                return null;
        }

        public async Task<IList<Table>> GetAllTables(int restaurantId)
        {
            return await _context.Tables.Where(t => t.Room.RestaurantId == restaurantId ).ToListAsync();
        }

        public async Task<IEnumerable<Table>> GetAllTablesFilteredBySeatsCount(int restaurantId)
        {
            return await _context.Tables.Where(t => t.Room.RestaurantId == restaurantId ).OrderByDescending(t => t.Seats).ToListAsync();
        }

        public async Task<Table> GetTableById(int id, int restaurantId)
        {
            return await _context.Tables.FirstOrDefaultAsync(t => t.Id == id && t.Room.RestaurantId == restaurantId );
        }

        public async Task<IEnumerable<Table>> GetTablesByRoomId(int roomId, int restaurantId)
        {
            return await _context.Tables.Where(t => t.RoomId == roomId && t.Room.RestaurantId == restaurantId ).ToListAsync();
        }

        public async Task<bool> TableCorrect(Table table, int restaurantId)
        {
            return await _context.Rooms.AnyAsync(r => r.Id == table.RoomId && r.RestaurantId == restaurantId ); 
        }
    }
}
