using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetDapperWebApi.Entities
{
    public class UserRole
    {
        [Key]
        public int UserId { get; set; }

        [Key]
        public int RoleId { get; set; }

          // Navigation Properties
        [JsonIgnore]
        public virtual User User { get; set; }
        [JsonIgnore]
        public virtual Role Role { get; set; }
    }
}