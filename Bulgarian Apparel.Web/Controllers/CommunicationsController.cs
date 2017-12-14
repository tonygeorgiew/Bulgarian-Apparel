using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bulgarian_Apparel.Web.Controllers
{
    public class CommunicationsController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return this.View();
        }
    }
}