using NetDapperWebApi.Entities;

namespace NetDapperWebApi.Common.Interfaces
{
    public interface IRoomService
    {
        Task<Room> CreateRoom(Room Room);
        Task<Room> GetRoom(int id);
        Task<IEnumerable<Room>> GetRooms();
        Task<Room> UpdateRoom(int id, Room Room);
        Task<bool> DeleteRoom(int id);
    }
}