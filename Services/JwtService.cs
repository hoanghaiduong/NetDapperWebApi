
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using NetDapperWebApi.Common.Interfaces;

namespace NetDapperWebApi.Services
{
    public class JwtService : IJwtService
    {
        private readonly string _accessTokenKey;
        private readonly string _refreshTokenKey;
        private readonly string _issuer;
        private readonly string _audience;

        public JwtService(IConfiguration configuration)
        {
            _accessTokenKey = configuration["AppSettings:AccessTokenSecret"]!;
            _refreshTokenKey = configuration["AppSettings:RefreshTokenSecret"]!;
            _issuer = configuration["AppSettings:Issuer"]!;
            _audience = configuration["AppSettings:Audience"]!;
        }


        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_accessTokenKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(15), // Access token expires quickly (e.g., 15 minutes)
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_refreshTokenKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.Aes128CbcHmacSha256);
            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddDays(7), // Refresh token expires later (e.g., 7 days)
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal ValidateAccessToken(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_accessTokenKey));
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false, // You can set this to true and provide a valid issuer
                    ValidateAudience = false, // You can set this to true and provide a valid audience
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero // Optional: reduce the time tolerance
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                return principal;
            }
            catch
            {
                return null; // Return null if token is invalid
            }
        }

        public ClaimsPrincipal ValidateRefreshToken(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_refreshTokenKey));
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false, // You can set this to true and provide a valid issuer
                    ValidateAudience = false, // You can set this to true and provide a valid audience
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero // Optional: reduce the time tolerance
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                return principal;
            }
            catch
            {
                return null; // Return null if token is invalid
            }
        }
    }
}