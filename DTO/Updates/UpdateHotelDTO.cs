

namespace NetDapperWebApi.DTO.Updates
{
    public class UpdateHotelDTO : HotelDTO
    {

        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }

        public string? Email { get; set; } = null!;

        public IFormFile? Thumbnail { get; set; }
        public List<IFormFile?>? Images { get; set; }
        // ✅ Danh sách ảnh cũ cần giữ lại (chỉ chứa URL)
        public List<string>? KeptImages { get; set; }
        public int Stars { get; set; }
        public string? CheckinTime { get; set; }
        public string? CheckoutTime { get; set; }
    }
}