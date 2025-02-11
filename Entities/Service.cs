using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetDapperWebApi.Entities
{
    public class Service : BaseEntity<int>
    {

        [Required, MaxLength(255)]
        public string Name { get; set; }

        public string? Description { get; set; }
        public decimal Price { get; set; }

    }
}