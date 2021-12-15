using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nova_Land.ViewModels
{
    public class ProductSearchViewModel
    {
        public SearchParamsViewModel SearchParams { get; set; }
        public List<Nova_Land.Models.Product> Products { get; set; }
        public bool IsItemAdded { get; set; }
    }
}
