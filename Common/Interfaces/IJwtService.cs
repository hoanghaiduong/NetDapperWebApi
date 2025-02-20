
using System.Security.Claims;

namespace NetDapperWebApi.Common.Interfaces
{
    public interface IJwtService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal ValidateAccessToken(string token);
        ClaimsPrincipal ValidateRefreshToken(string token);
    }
}