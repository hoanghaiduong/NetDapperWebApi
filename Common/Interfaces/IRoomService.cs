using NetDapperWebApi.DTO;
using NetDapperWebApi.DTO.Creates.Rooms;
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
        Task<Room> UpdateRoom(int id, UpdateRoomDTO Room);
        Task<bool> DeleteRoom(int id);
    }
}