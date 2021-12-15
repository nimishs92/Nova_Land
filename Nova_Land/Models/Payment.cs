using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nova_Land.Models
{
    public enum PaymentType
    {
        DEBIT,
        CREDIT,
    }
    public class Payment
    {
        
        public int Id { get; set; }
        [Required]
        public PaymentType PaymentType { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Number  { get; set; }
        [Required]
        public int cvv { get; set; }
        [Required]
        public string exp_dt { get; set; }


    }
}
