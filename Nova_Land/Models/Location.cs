using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nova_Land.Models
{
    public class Location
    {
        public int  Id { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
