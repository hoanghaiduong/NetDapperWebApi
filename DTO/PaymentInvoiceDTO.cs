using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetDapperWebApi.Entities;

namespace NetDapperWebApi.DTO
{
    public class PaymentInvoiceDTO
    {
        [Required]
        public int PaymentId { get; set; }

        [Required]
        public int InvoiceId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        // Navigation Properties
        public virtual Payment Payment { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}