using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using NetDapperWebApi.Entities;

namespace NetDapperWebApi.DTO
{
    public class InvoiceDTO : BaseEntity<int>
    {

        public decimal TotalAmount { get; set; }

        public decimal PaidAmount { get; set; }
        public decimal DueAmount { get; set; }
        public string? Status { get; set; }
    }
}