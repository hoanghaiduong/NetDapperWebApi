
using System.Text.Json.Serialization;
using NetDapperWebApi.Entities;

namespace NetDapperWebApi.DTO
{
    public class UserDTO : BaseEntity<int>
    {

        public string Email { get; set; } = null!;
        [JsonIgnore]
        public string PasswordHash { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public bool? EmailVerified { get; set; } = false;
        public string? Avatar { get; set; } = string.Empty;
        public string? RefreshToken { get; set; } = string.Empty;
        public bool IsDisabled { get; set; } = false;
        public DateTime? LastLogin { get; set; }

    }
}