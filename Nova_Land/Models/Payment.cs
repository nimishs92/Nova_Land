using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nova_Land.Models
{
    public enum PaymentType
    {
        DEBIT,
        CREDIT
    }
    public class Payment
    {
        public int Id { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
