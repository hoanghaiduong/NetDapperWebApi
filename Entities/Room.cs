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
        [JsonIgnore]

        public int HotelId { get; set; }

        [JsonIgnore]
        public int RoomTypeId { get; set; }

        [Required, MaxLength(20)]
        public string RoomNumber { get; set; }


        public decimal Price { get; set; }
        public int Status { get; set; }

        // Navigation Properties
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Hotel? Hotel { get; set; } = null;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual RoomType? RoomType { get; set; } = null;
        // Ảnh sẽ được lấy từ bảng Images qua EntityType = "Room"
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<Image>? Images { get; set; } // Không map trực tiếp vào DB
    }


}