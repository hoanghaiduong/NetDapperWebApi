using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        public virtual Hotel Hotel { get; set; }
        public virtual RoomType RoomType { get; set; }
    }

  
}