using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Web.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bulgarian_Apparel.Web.Controllers
{
    public class OrdersController : Controller
    {

        public OrdersController()
        {

        }

        // GET: Orders
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakeAnOrder(ProductFormViewModel productForm)
        {
            var order = new Order()
            {

            };

            return RedirectToAction("Index", "Products");
        }

    }
}