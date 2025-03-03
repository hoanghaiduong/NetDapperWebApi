using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using NetDapperWebApi.Entities;

namespace NetDapperWebApi.DTO
{
    public class HotelDTO
    {
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public IFormFile? Thumbnail { get; set; }
        [Required]
        public List<IFormFile>? Images { get; set; }
        public int Stars { get; set; }
        public string CheckinTime { get; set; }
        public string CheckoutTime { get; set; }
    }

}