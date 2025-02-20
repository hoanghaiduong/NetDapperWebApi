using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetDapperWebApi.Entities
{
    public class Booking : BaseEntity<int>
    {

        [Required]
        public int UserId { get; set; }

        [Required]
        public int RoomId { get; set; }


        public DateTime? CheckInDate { get; set; } = DateTime.Now;


        public DateTime? CheckOutDate { get; set; } = DateTime.Now;


        public int? Status { get; set; }

        public decimal? TotalPrice { get; set; } = 0.0m;

        // Navigation Properties
        [JsonIgnore]
        public virtual User User { get; set; }
        [JsonIgnore]
        public virtual Room Room { get; set; }
    }
}