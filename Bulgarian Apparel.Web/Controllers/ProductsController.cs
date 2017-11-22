using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Services;
using Bulgarian_Apparel.Services.Contracts;
using Bulgarian_Apparel.Web.Models.Common;
using Bulgarian_Apparel.Web.Models.Products;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            var catalogue = new CatalogueViewModel()
            {
                Products = new List<ProductViewModel>(),
                Categories = new List<CategoryViewModel>(),
            };


            foreach (var product in products)
            {
                var catalogueProduct = this.mapper.Map<ProductViewModel>(product);
                catalogueProduct.Category.CategoryName = product.Category.Name.Replace(' ', '-');
                catalogueProduct.Category.SuperCategoryName = product.Category.SuperCategoryName.Replace(' ', '-') ?? null;
                this.mapper.Map(items.Single(i => i.ProductId == catalogueProduct.ProductId), catalogueProduct);
                catalogue.Products.Add(catalogueProduct);
            }


            catalogue.Categories = catalogue.Products.Select(p=>p.Category).DistinctBy(c=>c.CategoryName).ToList();

            return View(catalogue);
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
            var sizes = this.sizesService.GetAll().ProjectTo<SizeViewModel>().ToList();
            var colors = this.colorsService.GetAll().ProjectTo<ColorViewModel>().ToList();
            var categories = this.categoriesService.GetAll().ToList();

            List<SelectListItem> listSizes = new List<SelectListItem>();
            foreach (var size in sizes)
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = size.Name,
                    Value = size.Id.ToString(),
                    Selected = size.IsSelected
                };
                listSizes.Add(selectList);
            }

            List<SelectListItem> listColors = new List<SelectListItem>();
            foreach (var color in colors)
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = color.Name,
                    Value = color.Id.ToString(),
                    Selected = color.IsSelected
                };
                listColors.Add(selectList);
            }


            var newProductAddVM = new NewProductAddVM()
            {
                Sizes = listSizes,
                Colors = listColors,
                Categories = categories
            };


            return this.View(newProductAddVM);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(NewProductAddVM productt)
        {
           
            var colors = new List<Color>();
            foreach (var color in productt.SelectedColors)
            {
                colors.Add(this.colorsService.GetColorForStringGuid(color));
            }
           
            var sizes = new List<Size>();
            foreach (var size in productt.SelectedSizes)
            {
                sizes.Add(this.sizesService.GetSizeForStringGuid(size));
            }

            var itemdbModel = new Item()
            {
                Price = productt.Price,
                Sizes = sizes,
                Colors = colors
            };
           
           
            var category = this.categoriesService.CategoryByStringId(productt.CategoriesId).Single();
            var productdbModel = new Product()
            {
                Name = productt.Name,
                Description = productt.Description,
                Stock = productt.Stock,
                Supplier = productt.Supplier,
                Hot = false,
                //CategoryId = category.Id
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

        public ActionResult Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                var all = this.productsService.GetAll().AsQueryable().ProjectTo<DeleteProductVM>().ToList();

                return this.PartialView("_ProductsResult", all);
            }

            var result = this.productsService
                .GetAll()
                .AsQueryable()
                .Where(product => product.Name.ToLower().Contains(query.ToLower()))
                .ProjectTo<DeleteProductVM>()
                .ToList();

            return this.PartialView("_ProductsResult", result);
        }

        public ActionResult SearchCatalogue(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                var all = this.productsService.GetAll().AsQueryable().ProjectTo<ProductViewModel>().ToList();

                return this.PartialView("_ProductsCatalogue", all);
            }

            var items = this.itemsService.GetAll().ToList();
            var products = this.productsService.GetAll().Where(product => product.Name.ToLower().Contains(query.ToLower())).ToList();
            var productsResult = new List<ProductViewModel>();

            foreach (var product in products)
            {
                productsResult.Add(this.mapper.Map<ProductViewModel>(product));
                this.mapper.Map(items.Single(i => i.ProductId == product.Id), productsResult.Single(pVM => pVM.ProductId == product.Id));
            }

            return this.PartialView("_ProductsCatalogue", productsResult);
        }

        public ActionResult SearchById(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new HttpNotFoundResult();
            }

            var result = this.productsService
                .GetAll()
                .Single(pro => pro.Id.ToString().ToLower().Contains(query.ToLower()));
            var product = mapper.Map<NewProductAddVM>(result);

            return this.PartialView("_ProductInformationResult", product);
        }



        [Authorize]
        public ActionResult RemoveProduct()
        {
            var products = this.productsService.GetAll().ProjectTo<DeleteProductVM>().ToList();
         

            return this.View(products);
        }


        [Authorize]
        public ActionResult RemoveProductById(string Id)
        {
            var products = this.productsService.GetAll().ProjectTo<DeleteProductVM>().ToList();


            return Content(Id);
        }
    }
}