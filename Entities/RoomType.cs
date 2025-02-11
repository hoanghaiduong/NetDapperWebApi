using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetDapperWebApi.Entities
{
    public class RoomType : BaseEntity<int>
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string? Description { get; set; }
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
     
    }
}