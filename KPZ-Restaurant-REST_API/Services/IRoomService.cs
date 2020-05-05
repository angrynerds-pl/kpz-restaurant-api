using System.Collections.Generic;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;

namespace KPZ_Restaurant_REST_API.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAllRooms(int restaurantId);

        Task<Room> CreateNewRoom(Room newRoom);
    }
}
