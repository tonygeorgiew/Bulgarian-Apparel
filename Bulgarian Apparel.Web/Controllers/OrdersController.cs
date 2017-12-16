using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bulgarian_Apparel.Common;
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
        private readonly IProductsService productsService;
        private readonly IOrdersService ordersService;
        private readonly IOrdersItemsService orderItemsService;
        private readonly IShoppingCartService shoppingCartService;
        private readonly IPaymentTypesService paymentTypesService;
        private readonly IUsersService usersService;
        private readonly IMapper mapper;

        public OrdersController(IProductsService productsService, IOrdersService ordersService, IOrdersItemsService orderItemsService,IShoppingCartService shoppingCartService, IUsersService usersService, IPaymentTypesService paymentTypesService, IMapper mapper)
        {
            this.productsService = productsService ?? throw new ArgumentNullException(GlobalConstants.productsServiceCheck, nameof(productsService));
            this.ordersService = ordersService ?? throw new ArgumentNullException(GlobalConstants.ordersServiceCheck, nameof(ordersService));
            this.orderItemsService = orderItemsService ?? throw new ArgumentNullException(GlobalConstants.ordersItemsServiceCheck, nameof(orderItemsService));
            this.shoppingCartService = shoppingCartService ?? throw new ArgumentNullException(GlobalConstants.shoppingCartServiceCheck, nameof(shoppingCartService));
            this.paymentTypesService = paymentTypesService ?? throw new ArgumentNullException(GlobalConstants.paymentTypesServiceCheck, nameof(paymentTypesService));
            this.usersService = usersService ?? throw new ArgumentNullException(GlobalConstants.usersServiceCheck, nameof(usersService));
            this.mapper = mapper ?? throw new ArgumentNullException(GlobalConstants.mapperCheck, nameof(mapper));
        }

        // GET: Orders
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult MyOrdersDetails()
        {
            var userId = this.User.Identity.GetUserId();
            var myOrders = this.orderItemsService.All().Where(oi=>oi.Customer.Id==userId).ProjectTo<OrderItemDetailsViewModel>().ToList();



            return this.View(myOrders);
        }

        // GET: Orders
        public ActionResult ValidateOrder(CheckoutFormViewModel orderForm)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();
                var userGuid = Guid.Parse(userId);
                var user = this.usersService.GetAll().Single(u => u.Id == userId);
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
                        Product = this.productsService.GetAll().Single(p=>p.Id==item.ProductId),
                        ProductColor = item.ProductColor,
                        ProductSize = item.ProductSize,
                        Payment = item.ProductPrice
                    };

                    order.OrderItems.Add(orderItem);
                }

                this.ordersService.Add(order);
                this.shoppingCartService.Delete(userCart);

                return this.View("Success", null, order.Id.ToString());
            }

            return this.View("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakeAnOrder(ProductFormViewModel productForm, string checkout, string addtocart)
        {
                

            //logic for adding order to orders repo
            if (!string.IsNullOrEmpty(checkout))
            {

                //add order to User's(antonii.g) cart thats at the Cart/Details;
                return this.Content(productForm.Product.ProductId.ToString());
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
                
                return this.View("Index");
            }

            return this.Content("skipped ifs");
        }

    }
}