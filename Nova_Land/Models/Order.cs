using Nova_Land.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nova_Land.Models
{

    public enum OrderStatus
    {
        NEW, 
        INPROGRESS,
        COMPLETED,
        FAILED,
        ONHOLD,
        CANCELLED
    }
    public class Order
    {
        public int ID { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public Nova_LandUser User { get; set; }
        public ICollection<OrderLineItem> OrderLineItems { get; set; }
        public bool IsCart { get; set; }
    }
}
