

using System.Data;
using System.Text.Json.Serialization;
using Dapper;
using NetDapperWebApi.Common.Interfaces;
using NetDapperWebApi.DTO;
using NetDapperWebApi.DTO.Creates;
using NetDapperWebApi.Entities;
using NetDapperWebApi.Models;

namespace NetDapperWebApi.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IDbConnection _db;
        private readonly IHotelService _hotelService;

        public UserService(ILogger<UserService> logger, IDbConnection db, IHotelService hotelService)
        {
            _logger = logger;
            _db = db;
            _hotelService = hotelService;
        }

        public async Task<User> CreateUser(CreateUserDTO user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Email", user.Email);
            parameters.Add("@Password", user.Password);
            parameters.Add("@PhoneNumber", user.PhoneNumber);
            parameters.Add("@FirstName", user.FirstName);
            parameters.Add("@LastName", user.LastName);
            parameters.Add("@EmailVerified", user.EmailVerified);
            parameters.Add("@Avatar", user.Avatar);
            parameters.Add("@RefreshToken", user.RefreshToken);
            parameters.Add("@IsDisabled", user.IsDisabled);
            parameters.Add("@LastLogin", user.LastLogin);
            parameters.Add("@HotelId", user.HotelId);
            // Gọi stored procedure Users_Create
            var result = await _db.QueryFirstOrDefaultAsync<User>(
                "Users_Create", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            var result = await _db.QueryFirstOrDefaultAsync<bool>(
                "Users_Delete", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        // Chỉ lấy danh sách relation cấp 1
        public async Task<PaginatedResult<User>> GetAllUsers(PaginationModel paginationModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PageNumber", paginationModel.PageNumber);
            parameters.Add("@PageSize", paginationModel.PageSize);
            parameters.Add("@Depth", paginationModel.Depth);
            parameters.Add("@Search", paginationModel.Search);

            int totalCount;
            List<User>? userRelations;

            using var multi = await _db.QueryMultipleAsync(
                "Users_GetAll", parameters, commandType: CommandType.StoredProcedure);

            // Đọc tổng số lượng user
            totalCount = await multi.ReadSingleAsync<int>();

            // Đọc danh sách user
            userRelations = (await multi.ReadAsync<User>()).ToList();//user with relations

            if (paginationModel.Depth >= 1)
            {
                // Đọc danh sách hotels
                var hotels = (await multi.ReadAsync<Hotel>()).ToList();


                var roles = (await multi.ReadAsync<Role>()).ToList();

                // Đọc danh sách bookings
                var bookings = (await multi.ReadAsync<Booking>()).ToList();
                foreach (var user in userRelations)
                {
                    user.Hotel = hotels.Where(h => h.Id == user.HotelId).FirstOrDefault();

                    user.Roles = [.. roles.Where(s => s.UserId == user.Id)];

                    user.Bookings = [.. bookings.Where(b => b.UserId == user.Id)];
                }
            }


            return new PaginatedResult<User>(userRelations, totalCount, paginationModel.PageNumber, paginationModel.PageSize);
        }


        public async Task<User> GetUserById(int id, int depth)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            parameters.Add("@Depth", depth);

            User? user;

            using (var multi = await _db.QueryMultipleAsync(
                "Users_GetByID", parameters, commandType: CommandType.StoredProcedure))
            {
                user = await multi.ReadSingleOrDefaultAsync<User>();


                if (depth == 1 && user != null)
                {

                    user.Roles = [.. (await multi.ReadAsync<Role>()).ToList().Where(s=>s.UserId==user.Id)];
                    user.Bookings = (await multi.ReadAsync<Booking>()).ToList();
                }
            } // `multi` sẽ tự động đóng ở đây
            if (depth == 1 && user?.HotelId != null)
            {
                user.Hotel = await _hotelService.GetHotel(user.HotelId ?? 0, 0);
            }
            // Sau khi đóng `multi`, gọi GetHotel bên ngoài using block
            if (depth >= 2 && user?.HotelId != null)
            {
                user.Hotel = await _hotelService.GetHotel(user.HotelId ?? 0, depth);
            }
            return user;
        }

        public async Task<User> UpdateUser(int id, UpdateUserDTO user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            parameters.Add("@PhoneNumber", user.PhoneNumber);
            parameters.Add("@FirstName", user.FirstName);
            parameters.Add("@LastName", user.LastName);
            parameters.Add("@EmailVerified", user.EmailVerified);
            parameters.Add("@Avatar", user.Avatar);
            parameters.Add("@RefreshToken", user.RefreshToken);
            parameters.Add("@IsDisabled", user.IsDisabled);
            parameters.Add("@LastLogin", user.LastLogin);
            parameters.Add("@HotelId", user.HotelId);

            var result = await _db.QueryFirstOrDefaultAsync<User>(
                "Users_Update", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }
    }


}