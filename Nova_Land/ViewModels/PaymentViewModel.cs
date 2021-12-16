using Nova_Land.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nova_Land.ViewModels
{
    public class PaymentViewModel
    {
        public Payment Payment { get; set; }
        public int OrderId { get; set; }

        public double TotalPrice { get; set; }
    }
}
