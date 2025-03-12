using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NetDapperWebApi.DTO.Updates
{
    public class UpdateRoomDTO
    {
        public int? HotelId { get; set; }

        public int? RoomTypeId { get; set; }

        public string? RoomNumber { get; set; }


        public IFormFile? Thumbnail { get; set; }

        public List<IFormFile>? Images { get; set; }
        // ✅ Danh sách ảnh cũ cần giữ lại (chỉ chứa URL)
        public List<string>? KeptImages { get; set; }


        public int? Floor { get; set; }
    
        [AllowedValues(values: [0, 1, 2])]
        public int? Status { get; set; }



    }
}
