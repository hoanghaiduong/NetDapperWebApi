using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetDapperWebApi.Entities
{
    public class Payment :BaseEntity<int>
    {
        
    [Required]
    public decimal Amount { get; set; }

    public int PaymentMethod { get; set; }
    public int Status { get; set; }
   
    public DateTime? PaymentDate { get; set; }
    }
}