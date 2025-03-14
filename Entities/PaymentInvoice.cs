using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NetDapperWebApi.Entities
{
    public class PaymentInvoice
    {
        [Required]
        public int PaymentId { get; set; }

        [Required]
        public int InvoiceId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        // Navigation Properties
        [JsonIgnore]
        public virtual Payment Payment { get; set; } 
        [JsonIgnore]
        public virtual Invoice Invoice { get; set; }
    }
}