using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using NetDapperWebApi.Entities;

namespace NetDapperWebApi.DTO
{
    public class ServiceDTO : BaseEntity<int>
    {

        public string Name { get; set; }

        public string? Description { get; set; }
        public decimal Price { get; set; }

    }
}