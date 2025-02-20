
using NetDapperWebApi.Entities;

namespace NetDapperWebApi.DTO
{
    public class RoomDTO : BaseEntity<int>
    {
        public string RoomNumber { get; set; }
        public string? Thumbnail { get; set; }
        public string? Images { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
    }
}