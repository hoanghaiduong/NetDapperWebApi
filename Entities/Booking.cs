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
        public string? BookingCode { get; set; } = Guid.NewGuid().ToString("N").ToUpper(); // Mã đặt phòng
        public string? Notes { get; set; } // Ghi chú đặt phòng
        public int Adults { get; set; }
        public int Children { get; set; }
        public int? RoomCount { get; set; }//số phòng
        public string? ArrivalTime { get; set; } // Thời gian đến dự kiến
        public DateTime? CheckInDate { get; set; } = DateTime.Now;
        public DateTime? CheckOutDate { get; set; } = DateTime.Now;


        public int? Status { get; set; }// Trạng thái đặt phòng (Status):   // 0: Chờ xác nhận 1: Đã xác nhận  2: Đã hủy 3: Đã hoàn thành

        public int? TotalPrice { get; set; } =0;

        public int? UserId { get; set; } // Id người đặt phòng
        // Navigation Properties
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual User? User { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual List<BookingRoomTypes> BookingRoomTypes { get; set; }


        //get Hotel From Booking
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Hotel? Hotel { get; set; }
    }
}