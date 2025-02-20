using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetDapperWebApi.Entities
{
    public class Role : BaseEntity<int>
    {


        [Required, MaxLength(255)]
        public string Name { get; set; }

        public string? Description { get; set; }


        [JsonIgnore]
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}