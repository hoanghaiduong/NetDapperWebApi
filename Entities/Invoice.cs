using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetDapperWebApi.Entities
{
    public class Invoice : BaseEntity<int>
    {

        [Required]
        public int BookingId { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        public decimal PaidAmount { get; set; }
        public decimal DueAmount { get; set; }
        public string? Status { get; set; }
     
          // Navigation Properties
        [JsonIgnore]
        public virtual Booking Booking { get; set; }

        public virtual List<PaymentInvoice>? PaymentInvoices { get; set; }
    }
}