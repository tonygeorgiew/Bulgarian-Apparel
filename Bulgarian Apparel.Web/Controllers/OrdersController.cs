using AutoMapper;
using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Services;
using Bulgarian_Apparel.Services.Contracts;
using Bulgarian_Apparel.Web.Models.Orders;
using Bulgarian_Apparel.Web.Models.Products;
using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Linq;
using System.Web.Mvc;

namespace Bulgarian_Apparel.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;
        private readonly IShoppingCartService shoppingCartService;
        private readonly IPaymentTypesService paymentTypesService;
        private readonly IUsersService usersService;
        private readonly IMapper mapper;

        public OrdersController(IOrdersService ordersService, IShoppingCartService shoppingCartService, IUsersService usersService, IPaymentTypesService paymentTypesService, IMapper mapper)
        {
            this.ordersService = ordersService;
            this.shoppingCartService = shoppingCartService;
            this.paymentTypesService = paymentTypesService;
            this.usersService = usersService;
            this.mapper = mapper;
        }

        // GET: Orders
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyOrdersDetails()
        {
            var userId = this.User.Identity.GetUserId();
            var myOrders = this.ordersService.GetAll().Where(o => o.Customer.Id == userId).SelectMany(c=>c.OrderItems).ToList();



            return View(myOrders);
        }

        // GET: Orders
        public ActionResult ValidateOrder(CheckoutFormViewModel orderForm)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();
                var userGuid = Guid.Parse(userId);
                var user = this.usersService.All().Single(u => u.Id == userId);
                var paymentId = Guid.Parse(orderForm.PreferedPayment);
                var chosenPaymentMethod = this.paymentTypesService.GetAll().Single(p=>p.Id == paymentId);
                

                var order = new Order()
                {
                    Customer = user
                };

                var userCart = this.shoppingCartService.GetAll().Single(u => u.UserId == userGuid);
                foreach (var item in userCart.ShoppingCartProducts)
                {
                    var orderItem = new OrderItem()
                    {
                        OrderedOn = DateTime.Now,
                        Address = orderForm.ShippingAddress,
                        Customer = user,
                        PaymentType = chosenPaymentMethod,
                        ProductId = item.ProductId,
                        ProductColor = item.ProductColor,
                        ProductSize = item.ProductSize,
                        Payment = item.ProductPrice
                    };

                    order.OrderItems.Add(orderItem);
                }

                this.ordersService.Add(order);
                this.shoppingCartService.Delete(userCart);

                return View("Success", null, order.Id.ToString());
            }

            return View("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakeAnOrder(ProductFormViewModel productForm, string checkout, string addtocart)
        {
                

            //logic for adding order to orders repo
            if (!string.IsNullOrEmpty(checkout))
            {

                //add order to User's(antonii.g) cart thats at the Cart/Details;
                return Content(productForm.Product.ProductId.ToString());
            }

            if (!string.IsNullOrEmpty(addtocart))
            {
                var cartProduct = new ShoppingCartProduct()
                {
                    ProductId = productForm.Order.ProductId,
                    ProductColor = productForm.Order.Color,
                    ProductSize = productForm.Order.Size,
                    ProductPrice = productForm.Order.Price,
                    PayedFor = true
                };

                var userId = Guid.Parse(User.Identity.GetUserId());

                var userCart = new ShoppingCart()
                {
                    UserId = userId
                };
                
                return View("Index");
            }

            return Content("skipped ifs");
        }

    }
}