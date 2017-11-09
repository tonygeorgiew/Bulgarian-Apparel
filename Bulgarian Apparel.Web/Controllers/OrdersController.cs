using AutoMapper;
using Bulgarian_Apparel.Services;
using Bulgarian_Apparel.Web.Models.Products;
using System.Web.Mvc;

namespace Bulgarian_Apparel.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;
        private readonly IMapper mapper;

        public OrdersController(IOrdersService ordersService, IMapper mapper)
        {
            this.ordersService = ordersService;
            this.mapper = mapper;
        }

        // GET: Orders
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakeAnOrder(ProductFormViewModel productForm, string makeorder, string addtocart)
        {
            //logic for adding order to orders repo
            if (!string.IsNullOrEmpty(makeorder))
            {

                //add order to User's(antonii.g) cart thats at the Cart/Details;
                return Content(productForm.Product.Id.ToString());
            }
            if (!string.IsNullOrEmpty(addtocart))
            {
                return View("Index");
            }

            return Content("skipped ifs");
        }

    }
}