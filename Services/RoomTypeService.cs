using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using NetDapperWebApi.Common.Interfaces;
using NetDapperWebApi.Entities;
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

        public async Task<RoomType> CreateRoomType(RoomType roomType)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@Name", roomType.Name);
            parameters.Add("@Description", roomType.Description);
            parameters.Add("@PricePerNight", roomType.PricePerNight);
            parameters.Add("@Capacity", roomType.Capacity);

            var result = await _db.QueryFirstOrDefaultAsync<RoomType>("RoomTypes_Create", parameters, commandType: CommandType.StoredProcedure);
            return result != null ? result : null;
        }

        public Task<bool> DeleteRoomType(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<RoomType> GetRoomType(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            var roomType = await _db.QueryFirstOrDefaultAsync<RoomType>(
                "RoomTypes_GetByID", parameters, commandType: CommandType.StoredProcedure);
            return roomType;
        }

        public async Task<IEnumerable<RoomType>> GetRoomTypes()
        {
            var roomTypes = await _db.QueryAsync<RoomType>(
               "RoomTypes_GetAll", commandType: CommandType.StoredProcedure);
            return roomTypes.ToList();
        }

        public async Task<RoomType> UpdateRoomType(int id, RoomType roomType)
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
            var updated = await GetRoomType(id);
            return updated;
        }
    }


}