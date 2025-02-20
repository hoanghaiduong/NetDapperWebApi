

using System.Data;
using System.Text.Json.Serialization;
using Dapper;
using NetDapperWebApi.Common.Interfaces;
using NetDapperWebApi.DTO;
using NetDapperWebApi.Entities;
using NetDapperWebApi.Models;

namespace NetDapperWebApi.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IDbConnection _db;

        public UserService(ILogger<UserService> logger, IDbConnection db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<User> CreateUser(User user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Email", user.Email);
            parameters.Add("@PasswordHash", user.PasswordHash);
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

        public async Task<PaginatedResult<UserRelations>> GetAllUsers(PaginationModel paginationModel)
        {
            return null;
        }

        public async Task<UserRelations> GetUserById(int id, int depth)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            parameters.Add("@Depth", depth);

            var multi = await _db.QueryMultipleAsync(
                "Users_GetByID", parameters, commandType: CommandType.StoredProcedure);
            var user = await multi.ReadSingleOrDefaultAsync<UserRelations>();
            if (user == null) return null;

            if (depth >= 1)
            {
                user.Hotel = await multi.ReadSingleOrDefaultAsync<HotelDTO>();
                user.Roles = (await multi.ReadAsync<RoleDTO>()).ToList();
                user.Bookings = (await multi.ReadAsync<BookingDTO>()).ToList();
            }

            if (depth >= 2)
            {
                
            }

            if (depth >= 3)
            {

            }
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", user.Id);
            parameters.Add("@Email", user.Email);
            parameters.Add("@PasswordHash", user.PasswordHash);
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
    public class UserHotel : UserDTO //depth ==1
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public HotelDTO Hotel { get; set; }
    }
    //depth ==2 lay room va roomtype

    public class UserRelations : UserHotel //result
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? BookingId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<BookingDTO>? Bookings { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? UserId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<RoleDTO>? Roles { get; set; }
    }
}