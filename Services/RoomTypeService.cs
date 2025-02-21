using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using NetDapperWebApi.Common.Interfaces;
using NetDapperWebApi.DTO;
using NetDapperWebApi.Entities;
using NetDapperWebApi.Models;
using WebApi.Context;

namespace NetDapperWebApi.Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IDbConnection _db;
        private readonly ILogger<RoomTypeService> _logger;

        public RoomTypeService(ILogger<RoomTypeService> logger, IDbConnection db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<RoomType> CreateRoomType(RoomTypeDTO roomType)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@Name", roomType.Name);
            parameters.Add("@Description", roomType.Description);
            parameters.Add("@PricePerNight", roomType.PricePerNight);
            parameters.Add("@Capacity", roomType.Capacity);

            var result = await _db.QueryFirstOrDefaultAsync<RoomType>("RoomTypes_Create", parameters, commandType: CommandType.StoredProcedure);
            return result != null ? result : null;
        }

        public async Task<bool> DeleteRoomType(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            var result = await _db.QueryFirstOrDefaultAsync<bool>(
                "RoomTypes_Delete", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<RoomType> GetRoomType(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            var roomType = await _db.QueryFirstOrDefaultAsync<RoomType>(
                "RoomTypes_GetByID", parameters, commandType: CommandType.StoredProcedure);
            return roomType;
        }

        public async Task<PaginatedResult<RoomType>> GetRoomTypes(PaginationModel paginationModel)
        {

            var multi = await _db.QueryMultipleAsync(
               "RoomTypes_GetAll", new
               {
                   paginationModel.PageNumber,
                   paginationModel.PageSize,
                   paginationModel.Depth
               }, commandType: CommandType.StoredProcedure);
            // Lấy tổng số bản ghi
            int totalCount = await multi.ReadSingleAsync<int>();
            // Lấy danh sách RoomTypes đã phân trang
            var roomTypes = (await multi.ReadAsync<RoomType>()).ToList();
            // Nếu Depth >= 1, lấy danh sách Rooms
            var rooms = paginationModel.Depth >= 1
                ? (await multi.ReadAsync<Room>()).ToList()
                : new List<Room>();

            // Nếu Depth >= 2, lấy danh sách Hotels
            var hotels = paginationModel.Depth >= 2
                ? (await multi.ReadAsync<Hotel>()).ToList()
                : new List<Hotel>();

            // Nhóm Rooms theo RoomTypeId
            var roomLookup = rooms.ToLookup(r => r.RoomTypeId);

            // Nếu Depth >= 2, gán Hotel vào Room
            if (paginationModel.Depth >= 2)
            {
                var hotelLookup = hotels.ToDictionary(h => h.Id);
                foreach (var room in rooms)
                {
                    if (hotelLookup.TryGetValue(room.HotelId, out var hotel))
                    {
                        room.Hotel = hotel;
                    }
                }
            }

            // Gán danh sách Rooms vào RoomType tương ứng
            foreach (var roomType in roomTypes)
            {
                roomType.Rooms = roomLookup[roomType.Id].ToList();
            }

            return new PaginatedResult<RoomType>(roomTypes, totalCount, paginationModel.PageSize, paginationModel.PageNumber);
        }

        public async Task<RoomType> GetRoomTypeWithRooms(int id, int depth)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            // Gọi stored procedure lấy RoomType
            var multi = await _db.QueryMultipleAsync(
                "RoomTypes_GetByID_WithRooms",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            // Lấy RoomType
            var roomType = await multi.ReadFirstOrDefaultAsync<RoomType>();

            if (roomType == null) return null;

            // Nếu depth >= 1, lấy danh sách Room
            if (depth >= 1)
            {
                var rooms = await multi.ReadAsync<Room>();
                roomType.Rooms = rooms.ToList();

                // Nếu depth >= 2, lấy thêm thông tin Hotel cho từng Room
                if (depth < 2) // Nếu depth < 2, set Hotel = null để ẩn nó
                {
                    foreach (var room in roomType.Rooms)
                    {
                        room.Hotel = null;
                    }
                }
                else if (depth == 2)
                {
                    foreach (var room in roomType.Rooms)
                    {
                        var hotelParams = new DynamicParameters();
                        hotelParams.Add("@Id", room.HotelId);
                        // Lấy thông tin Hotel của Room
                        room.Hotel = await _db.QueryFirstOrDefaultAsync<Hotel>(
                            "Hotels_GetByID", hotelParams, commandType: CommandType.StoredProcedure);
                    }
                }
            }
            return roomType;
        }


        public async Task<RoomType> UpdateRoomType(RoomTypeDTO roomType)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", roomType.Id);
            parameters.Add("@Name", roomType.Name);
            parameters.Add("@Description", roomType.Description);
            parameters.Add("@PricePerNight", roomType.PricePerNight);
            parameters.Add("@Capacity", roomType.Capacity);

            var result = await _db.QueryFirstOrDefaultAsync<int>(
               "RoomTypes_Update", parameters, commandType: CommandType.StoredProcedure);
            if (result == 0) return null;
            var updated = await GetRoomType(roomType.Id);
            return updated;
        }


    }



}