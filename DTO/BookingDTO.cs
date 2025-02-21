using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using NetDapperWebApi.Entities;

namespace NetDapperWebApi.DTO
{
    public class BookingDTO : BaseEntity<int>
    {
        public DateTime? CheckInDate { get; set; } = DateTime.Now;


        public DateTime? CheckOutDate { get; set; } = DateTime.Now;

        public int? Status { get; set; }

        public decimal? TotalPrice { get; set; } = 0.0m;
        [JsonIgnore]
        public int? UserId { get; set; }

    }
}