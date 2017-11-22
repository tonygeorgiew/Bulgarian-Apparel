using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bulgarian_Apparel.Web.Models.Products
{
    public class DeleteProductVM : IMapFrom<Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Supplier { get; set; }
    }
}