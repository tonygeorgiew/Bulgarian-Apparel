using AutoMapper.QueryableExtensions;
using Bulgarian_Apparel.Common;
using Bulgarian_Apparel.Services;
using Bulgarian_Apparel.Services.Contracts;
using Bulgarian_Apparel.Services.Data.Contracts;
using Bulgarian_Apparel.Web.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bulgarian_Apparel.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IProductsService productsService;
        private readonly IWishlistsService wishesService;

        public UsersController(IUsersService usersService, IProductsService productsService, IWishlistsService wishesService)
        {
            this.usersService = usersService ?? throw new ArgumentNullException(GlobalConstants.usersServiceCheck, nameof(usersService));
            this.productsService = productsService ?? throw new ArgumentNullException(GlobalConstants.productsServiceCheck, nameof(productsService));
            this.wishesService = wishesService ?? throw new ArgumentNullException(GlobalConstants.wishesServiceCheck, nameof(wishesService));
        }

        public ActionResult Index()
        {
            var users = this.usersService
                .GetAll()
                .ProjectTo<UserViewModel>()
                .ToList();

            return this.View(users);
        }

        public ActionResult Details(string id)
        {
            if (id == null || id == string.Empty)
            {
                return this.View("Error");
            }

            var user = this.usersService.GetUserById(id)
                .ProjectTo<UserViewModel>()
                .SingleOrDefault();
            // TO DO: Implement user wishlist
            //  var wishlist = this.wishesService.GetAll().Select(w => w.Customer.Id == user.Id);

            if (user == null)
            {
                return this.View("Error");
            }

            return this.View(user);
        }
    }
}