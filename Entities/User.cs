using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetDapperWebApi.Entities
{
    public class User : BaseEntity<int>
    {

        [Required, EmailAddress, MaxLength(255)]
        public string Email { get; set; } = null!;

        [Required, MaxLength(255)]
        public string PasswordHash { get; set; } = null!;

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [MaxLength(50)]
        public string? FirstName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? LastName { get; set; } = string.Empty;

        public bool? EmailVerified { get; set; }=false;
        public string? Avatar { get; set; }=string.Empty;
        public string? RefreshToken { get; set; } = string.Empty;
        public bool IsDisabled { get; set; } = false;
        public DateTime? LastLogin { get; set; }
        public int? HotelId { get; set; }


        // Navigation Properties
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}