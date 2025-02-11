using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetDapperWebApi.Entities
{
    public class ServiceUsage : BaseEntity<int>
    {

        [Required]
        public int BookingId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        public int Quantity { get; set; } = 1;
        public decimal TotalPrice { get; set; }
        public DateTime UsedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual Booking Booking { get; set; }
        public virtual Service Service { get; set; }
    }
}