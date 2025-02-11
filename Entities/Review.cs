using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetDapperWebApi.Entities
{
    public class Review : BaseEntity<int>
    {
        [Required]
        public int BookingId { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required, MaxLength(500)]
        public string Comment { get; set; }

    

        // Navigation Properties
        public virtual Booking Booking { get; set; }
    }
}