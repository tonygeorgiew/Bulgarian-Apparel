using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Services;
using Bulgarian_Apparel.Web.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bulgarian_Apparel.Web.Controllers
{
    public class ProductsController : Controller
    {

        private readonly IProductsService productsService;
        private readonly IMapper mapper;

        public ProductsController(IProductsService productsService, IMapper mapper)
        {
            this.productsService = productsService;
            this.mapper = mapper;
        }

        // GET: Products
        public ActionResult Index()
        {

            var products = this.productsService
                .GetAll()
                .ProjectTo<ProductViewModel>()
                .ToList();

            return View(products);
        }

        // [HttpGet]
        // public JsonResult GetProducts()
        // {
        //
        //     return Json(products);
        // }

        public ActionResult Details(int? id)
        {
            var product = this.productsService.GetAll().Where(p => p.Id == id).SingleOrDefault();
            var productVM = mapper.Map<ProductViewModel>(product);

            return View(productVM);
        }
    }
}