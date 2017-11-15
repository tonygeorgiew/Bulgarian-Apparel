using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Services;
using Bulgarian_Apparel.Web.Models.Home;
using Bulgarian_Apparel.Web.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bulgarian_Apparel.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsService productsService;
        private readonly IMapper mapper;

        public HomeController(IProductsService productsService, IMapper mapper)
        {
            this.productsService = productsService;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            //Needs refractoring
            var hotProducts = this.productsService
                .GetAll()
              //.Where(p => p.Hot == true)
                .ToList();

            var mappedHotProducts = new List<HotProductVM>();
            foreach (var item in hotProducts)
            {
                mappedHotProducts.Add(mapper.Map<HotProductVM>(item));
              
            }
            


            return View(mappedHotProducts);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult View1()
        {
            return View();
        }
    }
}