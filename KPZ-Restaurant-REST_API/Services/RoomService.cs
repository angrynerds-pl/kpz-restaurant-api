using System.Collections.Generic;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;
using KPZ_Restaurant_REST_API.Repositories;

namespace KPZ_Restaurant_REST_API.Services
{
    public class RoomService : IRoomService
    {

        private IRoomsRepository _roomsRepo;

        public RoomService(IRoomsRepository roomsRepo)
        {
            _roomsRepo = roomsRepo;
        }

        public async Task<IEnumerable<Room>> GetAllRooms(int restaurantId)
        {
            return await _roomsRepo.GetAllRooms(restaurantId);
        }

        public async Task<Room> CreateNewRoom(Room newRoom)
        {
            var roomCorrect = await _roomsRepo.RoomCorrect(newRoom);
            if (roomCorrect)
            {
                await _roomsRepo.Add(newRoom);
                await _roomsRepo.SaveAsync();
                return newRoom;
            }
            else
                return null;
        }

        public async Task<Room> DeleteRoomById(int roomId, int restaurantId)
        {
            var deletedRoom = await _roomsRepo.DeleteRoomById(roomId, restaurantId);
            if (deletedRoom != null)
            {
                await _roomsRepo.SaveAsync();
                return deletedRoom;
            }
            else
                return null;
        }
    }
}
