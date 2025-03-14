

using System.Text.Json.Serialization;

namespace NetDapperWebApi.DTO
{
    public class BookingDTO
    {
        [JsonIgnore]
        public string? BookingCode { get; set; }
        public string? Notes { get; set; } // Ghi chú đặt phòng
        public int Adults { get; set; }
        public int Children { get; set; }
        [JsonIgnore]
        public int RoomCount { get; set; }//số phòng
        public string? ArrivalTime { get; set; } // Thời gian đến dự kiến
        public DateTime? CheckInDate { get; set; } = DateTime.Now;
        public DateTime? CheckOutDate { get; set; } = DateTime.Now;

        public int? Status { get; set; }// Trạng thái đặt phòng (Status):   // 0: Chờ xác nhận 1: Đã xác nhận  2: Đã hủy 3: Đã hoàn thành

        public int? BasePrice { get; set; } = 0;
        public int? UserId { get; set; }
        

    }
}