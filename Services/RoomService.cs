
using System.Data;
using Dapper;
using NetDapperWebApi.Common.Interfaces;
using NetDapperWebApi.Entities;

namespace NetDapperWebApi.Services
{
    public class RoomService : IRoomService
    {
        private readonly IDbConnection _db;
        private readonly ILogger<RoomService> _logger;

        public RoomService(IDbConnection db, ILogger<RoomService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<Room> CreateRoom(Room room)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@HotelID", room.HotelId);
            parameters.Add("@RoomTypeId", room.RoomTypeId);
            parameters.Add("@RoomNumber", room.RoomNumber);
            parameters.Add("@Thumbnail", room.Thumbnail);
            parameters.Add("@Images", room.Images);
            parameters.Add("@Price", room.Price);
            parameters.Add("@Status", room.Status);
            

            var result = await _db.QueryFirstOrDefaultAsync<Room>(
                "Rooms_Create", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<bool> DeleteRoom(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            var result = await _db.QueryFirstOrDefaultAsync<bool>(
                "Rooms_Delete", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<Room> GetRoom(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            var room = await _db.QueryFirstOrDefaultAsync<Room>(
                "Rooms_GetByID", parameters, commandType: CommandType.StoredProcedure);
            return room;
        }

        public async Task<IEnumerable<Room>> GetRooms()
        {
            var rooms = await _db.QueryAsync<Room>("Rooms_GetAll", commandType: CommandType.StoredProcedure);
            return rooms;
        }

        public async Task<Room> UpdateRoom(int id, Room room)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", room.Id);
            parameters.Add("@HotelID", room.HotelId);
            parameters.Add("@RoomTypeId", room.RoomTypeId);
            parameters.Add("@RoomNumber", room.RoomNumber);
            parameters.Add("@Thumbnail", room.Thumbnail);
            parameters.Add("@Images", room.Images);
            parameters.Add("@Price", room.Price);
            parameters.Add("@Status", room.Status);
            parameters.Add("@UpdatedAt", room.UpdatedAt);

            var result = await _db.QueryFirstOrDefaultAsync<Room>(
                "Rooms_Update", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}