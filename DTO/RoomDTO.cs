
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
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
        [Required]
        public IFormFile? Thumbnail { get; set; }
        [Required]
        public List<IFormFile>? Images { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [AllowedValues(values: [0, 1, 2])]
        public int Status { get; set; }


    }
}