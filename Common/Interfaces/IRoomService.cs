using NetDapperWebApi.DTO;
using NetDapperWebApi.Entities;
using NetDapperWebApi.Models;
using NetDapperWebApi.Services;

namespace NetDapperWebApi.Common.Interfaces
{
    public interface IRoomService
    {
        Task<Room> CreateRoom(RoomDTO Room);
        Task<Room> GetRoom(int id);
        Task<PaginatedResult<Room>> GetRooms(PaginationModel dto);
        Task<Room> UpdateRoom(int id, Room Room);
        Task<bool> DeleteRoom(int id);
    }
}