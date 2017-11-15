using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Services;
using Bulgarian_Apparel.Services.Contracts;
using Bulgarian_Apparel.Web.Models.Products;
using Microsoft.AspNet.Identity;
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
        private readonly IColorsService colorsService;
        private readonly ICategoriesService categoriesService;
        private readonly IMapper mapper;

        public ProductsController(IProductsService productsService, IItemsService itemsService, ISizesService sizesService, IColorsService colorsService, ICategoriesService categoriesService, IMapper mapper)
        {
            this.productsService = productsService;
            this.itemsService = itemsService;
            this.sizesService = sizesService;
            this.colorsService = colorsService;
            this.categoriesService = categoriesService;
            this.mapper = mapper;
        }

        // GET: Products
        [HttpGet]
        public ActionResult Index()
        {
            var items = this.itemsService.GetAll().ToList();
            var products = this.productsService.GetAll().ToList();
            var productsVM = new List<ProductViewModel>();

            foreach (var product in products)
            {
                productsVM.Add(this.mapper.Map<ProductViewModel>(product));
                this.mapper.Map(items.Single(i => i.ProductId == product.Id), productsVM.Single(pVM => pVM.ProductId == product.Id));
            }

            return View(productsVM);
        }

      public ActionResult ViewProduct(string id)
      {
            id = id.ToUpper();
            var product = this.productsService.ProducttByStringId(id).Single();
            var item = this.itemsService.GetAll().Single(i => i.ProductId == product.Id);

            var productVM = new ProductFormViewModel()
            {
                Product = this.mapper.Map<ProductViewModel>(product)
            };

            this.mapper.Map(item, productVM.Product);

            return View(productVM);
      }

        [Authorize]
        [HttpGet]
        public ActionResult AddProduct()
        {
            var sizes = this.sizesService.GetAll().ToList();
            var colors = this.colorsService.GetAll().ToList();

            var newProductAddVM = new NewProductAddVM()
            {
                Sizes = sizes,
                Colors = colors
            };

            return this.View(newProductAddVM);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(NewProductAddVM productt)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            var itemdbModel = this.mapper.Map<Item>(productt);
            var productdbModel = new Product()
            {
                Name = productt.Name,
                Description = productt.Description,
                Stock = productt.Stock,
                Supplier = productt.Supplier
            };

            itemdbModel.ProductId = productdbModel.Id;
            productdbModel.ItemId = itemdbModel.Id;

            foreach (var image in productt.Images)
            {
                var imageToAdd = new Image()
                {
                    Resource = image
                };

                productdbModel.Images.Add(imageToAdd);
            }


            this.productsService.Add(productdbModel);
            this.itemsService.Add(itemdbModel);

            return this.RedirectToAction("ViewProduct", new { id = productdbModel.Id });
        }


    }
}