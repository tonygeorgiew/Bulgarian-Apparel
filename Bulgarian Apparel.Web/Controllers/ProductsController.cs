using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Services;
using Bulgarian_Apparel.Services.Contracts;
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
        private readonly IItemsService itemsService;
        private readonly ISizesService sizesService;
        private readonly IMapper mapper;

        public ProductsController(IProductsService productsService, IItemsService itemsService, ISizesService sizesService,IMapper mapper)
        {
            this.productsService = productsService;
            this.itemsService = itemsService;
            this.sizesService = sizesService;
            this.mapper = mapper;
        }

        // GET: Products
        public ActionResult Index()
        {
            var product = this.productsService.GetAll().Single();
            var item = this.itemsService.GetAll().Single();
            var viewModel = this.mapper.Map<ProductViewModel>(product);
            this.mapper.Map(item, viewModel);

            var sizesAvailable = this.sizesService.GetAll();
            var form = new ProductFormViewModel()
            {
                Product = viewModel
            };


            return View(form);
        }

        // [HttpGet]
        // public JsonResult GetProducts()
        // {
        //
        //     return Json(products);
        // }

       // public ActionResult Details(int? id)
       // {
       //     var product = this.productsService.GetAll().Where(p => p.Id == id).SingleOrDefault();
       //     var productVM = mapper.Map<ProductViewModel>(product);
       //
       //     return View(productVM);
       // }
    }
}