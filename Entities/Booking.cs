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
        public string BookingCode { get; set; } = Guid.NewGuid().ToString("N").ToUpper(); // Mã đặt phòng
        [Required]
        public int UserId { get; set; }

        [Required]
        public int RoomId { get; set; }
        public string Notes { get; set; } // Ghi chú đặt phòng
        public int Adults { get; set; }
        public int Children { get; set; }
        public int RoomCount { get; set; }

        public DateTime? CheckInDate { get; set; } = DateTime.Now;


        public DateTime? CheckOutDate { get; set; } = DateTime.Now;


        public int? Status { get; set; }// Trạng thái đặt phòng (Status):   // 0: Chờ xác nhận 1: Đã xác nhận  2: Đã hủy 3: Đã hoàn thành

        public decimal? TotalPrice { get; set; } = 0.0m;

        // Navigation Properties
        [JsonIgnore]
        public virtual User User { get; set; }
        [JsonIgnore]
        public virtual Room Room { get; set; }
    }
}