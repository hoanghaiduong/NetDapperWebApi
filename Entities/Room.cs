using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using NetDapperWebApi.DTO;

namespace NetDapperWebApi.Entities
{
    public class Room : BaseEntity<int>
    {
        [Required]
        public int HotelId { get; set; }

        [Required]
        public int RoomTypeId { get; set; }

        [Required, MaxLength(20)]
        public string RoomNumber { get; set; }

        public string? Thumbnail { get; set; }
        public string? Images { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }

        // Navigation Properties
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Hotel? Hotel { get; set; } = null;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual RoomType? RoomType { get; set; } = null;
    }


}