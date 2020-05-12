using System.Collections.Generic;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;

namespace KPZ_Restaurant_REST_API.Services
{
    public interface ITableService
    {
        Task<Table> UpdateTable(int id, Table table, int restaurantId);
        Task<Table> AddNewTable(Table table);
        Task<Table> RemoveTableById(int id, int restaurantId);
        Task<IEnumerable<Table>> GetAllTablesByRoomId(int roomId, int restaurantId);
        Task<Table> GetTableById(int id, int restaurantId);
    } 
}