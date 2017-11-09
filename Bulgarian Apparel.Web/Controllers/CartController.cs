using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Web.Models.Cart;
using Bulgarian_Apparel.Web.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bulgarian_Apparel.Web.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details()
        {
            var cart = new CartViewModel
            {
                ProductsOrdered = new List<CartItemViewModel>()
            };

            var product = new CartItemViewModel
            {
                ProductName = "Casual shirt",
                Brand = "H&M",
                Size = "XL",
                Color = "Blue",
                Price = 100.00,
                ShippingAddress = "Bulgaria, Sofia, Sofia-town, ul.Studentski grad blok 52-b, apartment number 216"
            };

            cart.ProductsOrdered.Add(product);
            cart.ProductsOrdered.Add(product);
            cart.ProductsOrdered.Add(product);

            return View(cart);
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult CheckoutNow(OrderViewModel order)
        {


            return View();

        }
    }
}