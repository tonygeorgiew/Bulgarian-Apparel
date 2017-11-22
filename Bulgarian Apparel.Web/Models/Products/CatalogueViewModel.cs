using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bulgarian_Apparel.Web.Models.Products
{
    public class CatalogueViewModel
    {
        public ICollection<ProductViewModel> Products { get; set; }
               
        public ICollection<CategoryViewModel> Categories { get; set; }
    }
}