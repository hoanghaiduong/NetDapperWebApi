
using System.Data;
using Dapper;
using NetDapperWebApi.Common.Interfaces;
using NetDapperWebApi.DTO;
using NetDapperWebApi.Entities;
using NetDapperWebApi.Models;

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

        public async Task<Room> CreateRoom(RoomDTO room)
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

        public async Task<PaginatedResult<Room>> GetRooms(PaginationModel paginationModel)
        {
            var parameters = new
            {
                PageNumber = paginationModel.PageNumber,
                PageSize = paginationModel.PageSize,
                Depth = paginationModel.Depth,
                Search = paginationModel.Search
            };

            using (var multi = await _db.QueryMultipleAsync("Rooms_GetAll", parameters, commandType: CommandType.StoredProcedure))
            {
                var totalCount = await multi.ReadFirstOrDefaultAsync<int>(); // Đọc TotalCount
                var rooms = (await multi.ReadAsync<Room>()).ToList(); // Đọc danh sách Rooms

                List<Hotel> hotels = new();
                List<RoomType> roomTypes = new();

                if (paginationModel.Depth >= 1)
                {
                    hotels = (await multi.ReadAsync<Hotel>()).ToList();
                    roomTypes = (await multi.ReadAsync<RoomType>()).ToList();

                    foreach (var room in rooms)
                    {
                        room.Hotel = hotels.FirstOrDefault(s => s.Id == room.HotelId);
                        room.RoomType = roomTypes.FirstOrDefault(s => s.Id == room.RoomTypeId);
                    }
                }

                return new PaginatedResult<Room>(rooms, totalCount, paginationModel.PageNumber, paginationModel.PageSize);
            }
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