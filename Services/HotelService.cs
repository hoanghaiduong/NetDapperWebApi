using System.Data;
using System.Text.Json.Serialization;
using Dapper;
using NetDapperWebApi.Common.Interfaces;
using NetDapperWebApi.DTO;
using NetDapperWebApi.Entities;
using NetDapperWebApi.Models;

namespace NetDapperWebApi.Services
{
    public class HotelService : IHotelService
    {
        private readonly IDbConnection _db;
        private readonly ILogger<HotelService> _logger;

        public HotelService(IDbConnection db, ILogger<HotelService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<Hotel> CreateHotel(HotelDTO hotel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Name", hotel.Name);
            parameters.Add("@Address", hotel.Address);
            parameters.Add("@Phone", hotel.Phone);
            parameters.Add("@Email", hotel.Email);
            parameters.Add("@Thumbnail", hotel.Thumbnail);
            parameters.Add("@Images", hotel.Images);
            parameters.Add("@Stars", hotel.Stars);
            parameters.Add("@CheckinTime", hotel.CheckinTime);
            parameters.Add("@CheckoutTime", hotel.CheckoutTime);
            parameters.Add("@CreatedAt", hotel.CreatedAt);
            parameters.Add("@UpdatedAt", hotel.UpdatedAt);

            var result = await _db.QueryFirstOrDefaultAsync<Hotel>(
                "Hotels_Create", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<bool> DeleteHotel(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            var result = await _db.QueryFirstOrDefaultAsync<bool>(
                "Hotels_Delete", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<PaginatedResult<Hotel>> GetAllHotels(PaginationModel paginationModel)
        {

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PageNumber", paginationModel.PageNumber);
                parameters.Add("@PageSize", paginationModel.PageSize);
                parameters.Add("@Depth", paginationModel.Depth);
                parameters.Add("@Search", paginationModel.Search);

                using var multi = await _db.QueryMultipleAsync(
                  "Hotels_GetAll", parameters, commandType: CommandType.StoredProcedure);
                // Lấy tổng số bản ghi
                int totalCount = await multi.ReadSingleAsync<int>();

                // Lấy danh sách khách sạn (HotelWithRooms)
                var hotels = (await multi.ReadAsync<Hotel>()).ToList();

                // Nếu Depth >= 1, ta đọc thêm danh sách phòng
                if (paginationModel.Depth >= 1)
                {
                    var rooms = (await multi.ReadAsync<Room>()).ToList();
                    foreach (var hotel in hotels)
                    {
                        hotel.Rooms = rooms.Where(r => r.HotelId == hotel.Id).ToList();
                    }

                    // Nếu Depth == 2, ta đọc thêm danh sách RoomType và gán cho từng phòng
                    if (paginationModel.Depth == 2)
                    {
                        var roomTypes = (await multi.ReadAsync<RoomType>()).ToList();
                        foreach (var hotel in hotels)
                        {
                            foreach (var room in hotel.Rooms)
                            {
                                room.RoomType = roomTypes.FirstOrDefault(rt => rt.Id == room.RoomTypeId);
                            }
                        }
                    }

                }
                return new PaginatedResult<Hotel>(hotels, totalCount, paginationModel.PageNumber, paginationModel.PageSize);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error getting all hotels");
                throw ex;
            }

        }

        public async Task<Hotel> GetHotel(int id, int depth)
        {

            var parameters = new { Id = id, Depth = depth };

            using var multi = await _db.QueryMultipleAsync("Hotels_GetByID", parameters, commandType: CommandType.StoredProcedure);

            // Lấy thông tin khách sạn
            var hotel = await multi.ReadSingleOrDefaultAsync<Hotel>();
            if (hotel == null) return null; // Nếu không tìm thấy thì trả về null

            // Lấy danh sách phòng nếu depth >= 1
            if (depth >= 1)
            {
                var rooms = await multi.ReadAsync<Room>();
                hotel.Rooms = [.. rooms];
            }

            // Lấy danh sách RoomTypes nếu depth >= 2
            if (depth >= 2)
            {
                var roomTypes = await multi.ReadAsync<RoomType>();
                var roomTypeDict = roomTypes.ToDictionary(rt => rt.Id);

                foreach (var room in hotel.Rooms)
                {
                    if (roomTypeDict.TryGetValue(room.RoomTypeId, out var roomType))
                    {
                        room.RoomType = roomType;
                    }
                }
            }

            return hotel;
        }

        public async Task<Hotel> UpdateHotel(Hotel hotel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", hotel.Id);
            parameters.Add("@Name", hotel.Name);
            parameters.Add("@Address", hotel.Address);
            parameters.Add("@Phone", hotel.Phone);
            parameters.Add("@Email", hotel.Email);
            parameters.Add("@Thumbnail", hotel.Thumbnail);
            parameters.Add("@Images", hotel.Images);
            parameters.Add("@Stars", hotel.Stars);
            parameters.Add("@CheckinTime", hotel.CheckinTime);
            parameters.Add("@CheckoutTime", hotel.CheckoutTime);
            parameters.Add("@UpdatedAt", hotel.UpdatedAt);

            var result = await _db.QueryFirstOrDefaultAsync<Hotel>(
                "Hotels_Update", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }
    }

}