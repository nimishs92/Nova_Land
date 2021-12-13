using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nova_Land.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double UnitPrice { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public Category Category { get; set; }
        public ICollection<Location> Locations { get; set; }
    }
}
