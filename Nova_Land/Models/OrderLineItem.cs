using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nova_Land.Models
{
    public class OrderLineItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public double Price { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Order")]
        public int OrderRef { get; set; }
        public Order Order { get; set; }

    }
}
