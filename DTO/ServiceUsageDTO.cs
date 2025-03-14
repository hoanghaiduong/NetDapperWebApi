using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using NetDapperWebApi.Entities;

namespace NetDapperWebApi.DTO
{
    public class ServiceUsageDTO 
    {
        public int BookingId { get; set; }
        public int ServiceId {get;set;}
        public int Quantity { get; set; } = 1;
        [JsonIgnore]
        public int TotalPrice { get; set; }
        public DateTime UsedAt { get; set; } = DateTime.UtcNow;
    }
}