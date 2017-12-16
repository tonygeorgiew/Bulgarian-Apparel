using AutoMapper.QueryableExtensions;
using Bulgarian_Apparel.Auth;
using Bulgarian_Apparel.Services;
using Bulgarian_Apparel.Web.Areas.Admin.Models;
using Bulgarian_Apparel.Web.Models.Users;
using System;
using System.Linq;
using System.Web.Mvc;
using Bulgarian_Apparel.Common;

namespace Bulgarian_Apparel.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersManagerController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IAuthProvider authProvider;

        public UsersManagerController(IUsersService usersService, IAuthProvider authProvider)
        {
            this.usersService = usersService ?? throw new ArgumentNullException(GlobalConstants.usersServiceCheck, nameof(usersService));
            this.authProvider = authProvider ?? throw new ArgumentNullException(GlobalConstants.authProviderCheck, nameof(authProvider));
        }

        public ActionResult Index()
        {
            var users = this.usersService
                .GetAll()
                .ProjectTo<UserViewModel>()
                .ToList();

            return this.View(users);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddAdmin(string id)
        {
            this.authProvider.AddToRole(id, "Admin");

            return this.RedirectToAction("Index");
        }

        public ActionResult RemoveAdmin(string id)
        {
            this.authProvider.RemoveFromRole(id, "Admin");

            return this.RedirectToAction("Index");
        }

        public ActionResult DeleteUser(string id)
        {
            var user = this.usersService.GetUserById(id).SingleOrDefault();
            if (user == null)
            {
                return this.View("Error");
            }

            this.usersService.Delete(user);

            return this.RedirectToAction("Index");
        }
    }
}