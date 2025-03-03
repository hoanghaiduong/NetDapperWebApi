
using System.Data;
using NetDapperWebApi.Common.Interfaces;
using NetDapperWebApi.DTO;
using NetDapperWebApi.Models;
using Dapper;
using NetDapperWebApi.Entities;
using System.Security.Claims;
using Microsoft.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using NetDapperWebApi.DTO.Creates;

namespace NetDapperWebApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJwtService _jwtService;
        private readonly IDbConnection _db;
        private readonly IUserService _userService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IDbConnection db, ILogger<AuthService> logger, IJwtService jwtService, IUserService userService)
        {
            _db = db;
            _logger = logger;
            _jwtService = jwtService;
            _userService = userService;
        }

        public async Task<TokenModel> RefreshToken(RefreshTokenModel dto, string uid)
        {

            var user = await _userService.GetUserById(int.Parse(uid), 0);
            // Check if the provided refresh token matches the one in the database
            if (dto.RefreshToken != user.RefreshToken)
            {
                throw new Exception("RefreshToken Not Valid");
            }
            var principal = _jwtService.ValidateRefreshToken(dto.RefreshToken);
            // Generate new access token
            var newAccessToken = _jwtService.GenerateAccessToken(principal.Claims);

            // Optional: Generate new refresh token (to invalidate the old one)
            var newRefreshToken = _jwtService.GenerateRefreshToken();
            var updated = await _userService.UpdateUser(user.Id, new UpdateUserDTO
            {
                RefreshToken = newRefreshToken
            }) ?? throw new Exception();
            return new TokenModel(newAccessToken, newRefreshToken);
        }

        public async Task<AuthResponse> SignInAsync(AuthDTO dto)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Email", dto.Email);
                parameters.Add("@Password", dto.Password);

                var user = await _db.QueryFirstOrDefaultAsync<User>(
                    "Users_CheckLogin",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
                // 4. Kiểm tra kết quả
                if (user == null)
                {
                    throw new Exception("User creation failed.");
                }

                // 5. Tạo Access Token & Refresh Token
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),

                };
                var accessToken = _jwtService.GenerateAccessToken(claims);
                var refreshToken = _jwtService.GenerateRefreshToken();
                // 6. Cập nhật Refresh Token vào DB
                var updateParams = new DynamicParameters();
                updateParams.Add("@UserId", user.Id);
                updateParams.Add("@RefreshToken", refreshToken);
                await _db.ExecuteAsync("UPDATE Users SET RefreshToken = @RefreshToken WHERE Id = @UserId", updateParams);
                var token = new TokenModel(
                      accessToken, refreshToken
                );
                var newUser=await _userService.GetUserById(user.Id,1);
                // 7. Trả về AuthResponse
                return new AuthResponse
                {
                    User = newUser,
                    Token = token
                };
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> SignUpAsync(AuthDTO dto)
        {
            try
            {
                // // 1. Mã hóa mật khẩu
                // var hashPassword = HashService.EnhancedHashPassword(dto.Password);
                // 2. Chuẩn bị tham số cho stored procedure
                var parameters = new DynamicParameters();
                parameters.Add("@Email", dto.Email);
                parameters.Add("@Password", dto.Password);
                // 3. Gọi stored procedure để tạo user
                var user = await _db.QueryFirstOrDefaultAsync<User>("Users_Create", parameters, commandType: CommandType.StoredProcedure) ?? throw new Exception("User creation failed.");
                return user ?? null!;
            }
            catch (SqlException ex) when (ex.Message.Contains("already exists"))
            {
                throw new Exception("Email or phone number already exists.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Sign-up failed: {ex.Message}");
            }
        }

    }

}