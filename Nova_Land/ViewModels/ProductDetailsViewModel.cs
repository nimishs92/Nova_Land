using Nova_Land.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nova_Land.ViewModels
{
    public class ProductDetailsViewModel
    {
        public Product Product { get; set; }
        public bool IsItemAdded { get; set; }
    }
}
