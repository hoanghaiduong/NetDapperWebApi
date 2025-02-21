using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetDapperWebApi.Entities
{
    public class Hotel : BaseEntity<int>
    {
        [Required]
        public string Name { get; set; }

        public string? Address { get; set; }


        public string? Phone { get; set; }

        [Required]
        public string Email { get; set; } = null!;

        public string? Thumbnail { get; set; }
        public string? Images { get; set; }
        public int? Stars { get; set; }
        public string? CheckinTime { get; set; }
        public string? CheckoutTime { get; set; }

        // Navigation Properties
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual IList<Room>? Rooms { get; set; } = null;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual IList<User>? Users { get; set; } = null;//employee,guest,admin
    }

}