using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Web.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bulgarian_Apparel.Web.Models.Cart
{
    public class CartViewModel
    {
        public User User { get; set; }
        public IList<CartItemViewModel> ProductsOrdered { get; set; }
    }
}