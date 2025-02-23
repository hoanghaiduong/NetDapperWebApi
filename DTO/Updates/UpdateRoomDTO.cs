using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NetDapperWebApi.DTO.Creates.Rooms
{
    public class UpdateRoomDTO : RoomDTO
    {

        public new int HotelId { get; set; }

        public new int RoomTypeId { get; set; }

        public new string? RoomNumber { get; set; }


        public new IFormFile? Thumbnail { get; set; }

        public new List<IFormFile>? Images { get; set; }
    }
}
