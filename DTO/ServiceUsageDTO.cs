using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using NetDapperWebApi.Entities;

namespace NetDapperWebApi.DTO
{
    public class ServiceUsageDTO : BaseEntity<int>
    {
        public int Quantity { get; set; } = 1;
        public decimal TotalPrice { get; set; }
        public DateTime UsedAt { get; set; } = DateTime.UtcNow;
    }
}