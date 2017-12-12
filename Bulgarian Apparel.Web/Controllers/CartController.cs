using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Data.SaveContext;
using Bulgarian_Apparel.Services;
using Bulgarian_Apparel.Services.Contracts;
using Bulgarian_Apparel.Web.Infrastructure;
using Bulgarian_Apparel.Web.Models.Cart;
using Bulgarian_Apparel.Web.Models.Orders;
using Bulgarian_Apparel.Web.Models.Products;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bulgarian_Apparel.Web.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly IColorsService colorsService;
        private readonly ISizesService sizesService;
        private readonly IPaymentTypesService paymentTypesService;
        private readonly IUnitOfWork UoW;

        public CartController(IShoppingCartService shoppingCartService, IColorsService colorsService, ISizesService sizesService ,IPaymentTypesService paymentTypesService, IUnitOfWork UoW)
        {
            this.shoppingCartService = shoppingCartService;
            this.colorsService = colorsService;
            this.sizesService = sizesService;
            this.paymentTypesService = paymentTypesService;
            this.UoW = UoW;
        }

        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Details()
        {
            Guid userId = IdProccessor.GetGuidForStringId(User.Identity.GetUserId());
            if (this.shoppingCartService.GetAll().Any(u => u.UserId == userId))
            {
                var userCart = this.shoppingCartService.GetAll().Where(o => o.IsDeleted == false).Single(c => c.UserId == userId);

                return View(userCart);
            }

            return View("Empty");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProccessCart(ProductFormViewModel productForm, string checkout, string addtocart)
        {
            //logic for adding order to orders repo
            if (!string.IsNullOrEmpty(checkout))
            {
                var color = this.colorsService.GetColorForStringGuid(productForm.Order.ColorSelectedId);
                var size = this.sizesService.GetSizeForStringGuid(productForm.Order.SizeSelectedId);

                var cartProduct = new ShoppingCartProduct()
                {
                    ProductId = productForm.Product.ProductId,
                    ProductColor = color.Name,
                    ProductSize = size.Name,
                    ProductPrice = productForm.Product.Price,
                    PayedFor = false
                };

                var userId = IdProccessor.GetGuidForStringId(User.Identity.GetUserId());
                if (this.shoppingCartService.GetAll().Any(u => u.UserId == userId))
                {
                    var userCart = this.shoppingCartService.GetAll().Single(c => c.UserId == userId);
                    cartProduct.ShoppingCartId = userCart.Id;
                    userCart.ShoppingCartProducts.Add(cartProduct);

                    this.shoppingCartService.Update(userCart);
                }

                else
                {
                    var userCart = new ShoppingCart()
                    {
                        UserId = userId
                    };
                        
                    cartProduct.ShoppingCartId = userCart.Id;
                    userCart.ShoppingCartProducts.Add(cartProduct);

                    this.shoppingCartService.Add(userCart);
                }


                return RedirectToAction("CheckoutForm");
            }

            if (!string.IsNullOrEmpty(addtocart))
            {
                var color = this.colorsService.GetColorForStringGuid(productForm.Order.ColorSelectedId);
                var size = this.sizesService.GetSizeForStringGuid(productForm.Order.SizeSelectedId);

                var cartProduct = new ShoppingCartProduct()
                {
                    ProductId = productForm.Product.ProductId,
                    ProductColor = color.Name,
                    ProductSize = size.Name,
                    ProductPrice = productForm.Product.Price,
                    PayedFor = false
                };

                var userId = IdProccessor.GetGuidForStringId(User.Identity.GetUserId());

                //abstract this into a method called UserHasActiveCart(guid userid);
                if (this.shoppingCartService.GetAll().Any(u => u.UserId == userId))
                {
                    var userCart = this.shoppingCartService.GetAll().Single(c => c.UserId == userId);
                    cartProduct.ShoppingCartId = userCart.Id;
                    userCart.ShoppingCartProducts.Add(cartProduct);

                    this.shoppingCartService.Update(userCart);
                }

                else
                {
                    var userCart = new ShoppingCart()
                    {
                        UserId = userId
                    };

                    cartProduct.ShoppingCartId = userCart.Id;
                    userCart.ShoppingCartProducts.Add(cartProduct);

                    this.shoppingCartService.Add(userCart);
                }
                //end

                return RedirectToAction("Index", "Products");
            }

            return Content("skipped ifs");
        }

        public ActionResult CheckoutForm()
        {
            var userGuid = this.User.Identity.GetUserId();
            var totalPrice = this.shoppingCartService.CalculateTotalPriceForCart(userGuid);
            var paymentTypes = this.paymentTypesService.GetAll().ToList();
            var orderForm = new CheckoutFormViewModel()
            {
                PaymentType = paymentTypes,
                TotalPrice = totalPrice
            };

            return View(orderForm);
        }

        public ActionResult CheckoutNow(OrderViewModel order)
        {


            return View();

        }


        public ActionResult Empty()
        {

            var userStringId = this.User.Identity.GetUserId();
            var userCart = this.shoppingCartService.GetCartForUserId(userStringId).Single() ?? null;

            if(userCart != null)
            {
                this.shoppingCartService.Delete(userCart);
            }
            

            return RedirectToAction("Details");
        }
    }
}