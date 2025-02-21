using System.Security.Claims;
using NetDapperWebApi.DTO;
using NetDapperWebApi.Entities;
using NetDapperWebApi.Models;

namespace NetDapperWebApi.Common.Interfaces
{
    public interface IAuthService
    {
        Task<TokenModel> RefreshToken(RefreshTokenModel dto,string uid);
        Task<AuthResponse> SignInAsync(AuthDTO dto);
        Task<User> SignUpAsync(AuthDTO dto);
    }
}