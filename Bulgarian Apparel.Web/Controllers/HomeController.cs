using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bulgarian_Apparel.Common;
using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Services;
using Bulgarian_Apparel.Services.Contracts;
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
            this.productsService = productsService ?? throw new ArgumentNullException(GlobalConstants.productsServiceCheck, nameof(productsService));
            this.mapper = mapper ?? throw new ArgumentNullException(GlobalConstants.mapperCheck, nameof(mapper));
        }

        public ActionResult Index()
        {
            //Needs refractoring
            var hotProducts = this.productsService
                .GetAll()
              //.Where(p => p.Hot == true)
                .ToList();

            var mappedHotProducts = new List<HotProductViewModel>();
            var randomProductPicker = new Random();
            while (mappedHotProducts.Count <= 6)
            {
                mappedHotProducts.Add(mapper.Map<HotProductViewModel>(hotProducts[randomProductPicker.Next(0, hotProducts.Count)]));
            }

            return this.View(mappedHotProducts);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return this.View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return this.View();
        }
        public ActionResult View1()
        {
            return this.View();
        }
    }
}