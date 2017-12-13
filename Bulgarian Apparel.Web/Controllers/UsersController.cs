using AutoMapper.QueryableExtensions;
using Bulgarian_Apparel.Services;
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
        private readonly IProductsService projectsService;
        private readonly IWishlistsService wishesService;
        private readonly string projectsServiceCheck = "Project service is null";
        private readonly string usersServiceCheck = "Users service is null";
        private readonly string wishesServiceCheck = "Wishes service is null";

        public UsersController(IUsersService usersService, IProductsService projectsService, IWishlistsService wishesService)
        {
            this.usersService = usersService ?? throw new ArgumentNullException(usersServiceCheck, nameof(usersServiceCheck));
            this.projectsService = projectsService ?? throw new ArgumentNullException(projectsServiceCheck, nameof(projectsService));
            this.wishesService = wishesService ?? throw new ArgumentNullException(wishesServiceCheck, nameof(wishesService));
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
            var wishlist = this.wishesService.GetAll().Select(w => w.Customer.Id == user.Id);

            if (user == null)
            {
                return this.View("Error");
            }

            return this.View(user);
        }
    }
}