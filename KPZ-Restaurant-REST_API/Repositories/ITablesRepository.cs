using KPZ_Restaurant_REST_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Repositories
{
    public interface ITablesRepository : IRestaurantGeneric<Table>
    {
        Task<bool> CheckIfTablePresent(Table table);
        Task<Table> DeleteTableById(int id, int restaurantId);
        Task<IEnumerable<Table>> GetTablesByRoomId(int roomId, int restaurantId);
        Task<Table> GetTableById(int id, int restaurantId);
    }
}
