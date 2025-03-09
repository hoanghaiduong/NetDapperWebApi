
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using NetDapperWebApi.Common.Enums;
using NetDapperWebApi.Entities;

namespace NetDapperWebApi.DTO
{
    public class RoomDTO
    {
        [Required]
        public int HotelId { get; set; }
        [Required]
        public int RoomTypeId { get; set; }
        [Required]
        public string RoomNumber { get; set; }

        public int? Floor { get; set; }
        public decimal PricePerHour { get; set; }
        // [AllowedValues(values: [0, 1, 2,3,4,5])]
        public ERoomStatus Status { get; set; } = ERoomStatus.Ready;


        public IFormFile? Thumbnail { get; set; }

        public List<IFormFile>? Images { get; set; }



    }
}