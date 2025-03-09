using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using NetDapperWebApi.Entities;

namespace NetDapperWebApi.DTO
{
    public class RoomTypeDTO 
    {
        public int? HotelId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal PricePerNight { get; set; }
        public int NumberOfBathrooms { get; set; }
        public int NumberOfBeds { get; set; }
        public int SingleBed { get; set; }
        public int DoubleBed { get; set; }
        public int Capacity { get; set; }
        public int Sizes { get; set; }
        public IFormFile? Thumbnail { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}