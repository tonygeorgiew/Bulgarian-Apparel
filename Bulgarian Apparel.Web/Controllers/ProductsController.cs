using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bulgarian_Apparel.Auth;
using Bulgarian_Apparel.Common;
using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Services;
using Bulgarian_Apparel.Services.Contracts;
using Bulgarian_Apparel.Services.Data.Contracts;
using Bulgarian_Apparel.Web.Areas.Admin.Models;
using Bulgarian_Apparel.Web.Infrastructure;
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
        private readonly IUsersService usersService;
        private readonly IProductsService productsService;
        private readonly IItemsService itemsService;
        private readonly IWishlistsService wishesService;
        private readonly ISizesService sizesService;
        private readonly IColorsService colorsService;
        private readonly ICategoriesService categoriesService;
        private readonly IMapper mapper;
        private readonly IAuthProvider authProvider;

        public ProductsController(IUsersService usersService,IProductsService productsService, IItemsService itemsService, IWishlistsService wishesService, ISizesService sizesService, IColorsService colorsService, ICategoriesService categoriesService, IMapper mapper, IAuthProvider authProvider)
        {
            this.usersService = usersService ?? throw new ArgumentNullException(GlobalConstants.usersServiceCheck, nameof(usersService));
            this.productsService = productsService ?? throw new ArgumentNullException(GlobalConstants.productsServiceCheck, nameof(productsService));
            this.itemsService = itemsService ?? throw new ArgumentNullException(GlobalConstants.itemsServiceCheck, nameof(itemsService));
            this.wishesService = wishesService ?? throw new ArgumentNullException(GlobalConstants.wishesServiceCheck, nameof(wishesService));
            this.sizesService = sizesService ?? throw new ArgumentNullException(GlobalConstants.sizesServiceCheck, nameof(sizesService));
            this.colorsService = colorsService ?? throw new ArgumentNullException(GlobalConstants.colorsServiceCheck, nameof(colorsService));
            this.categoriesService = categoriesService ?? throw new ArgumentNullException(GlobalConstants.categoriesServiceCheck, nameof(categoriesService));
            this.mapper = mapper ?? throw new ArgumentNullException(GlobalConstants.mapperCheck, nameof(mapper));
            this.authProvider = authProvider ?? throw new ArgumentNullException(GlobalConstants.authProviderCheck, nameof(authProvider));
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
                catalogueProduct.Category = this.mapper.Map<CategoryViewModel>(product.Category);
                this.mapper.Map(items.Single(i => i.ProductId == catalogueProduct.ProductId), catalogueProduct);
                catalogue.Products.Add(catalogueProduct);
            }

            catalogue.Categories = catalogue.Products.Select(p => p.Category).DistinctBy(c => c.CategoryName).ToList();

            return this.View(catalogue);
        }

        public ActionResult ViewProduct(string id)
        {
            if(id == null)
            {
                return this.View("Error");
            }

            
            var product = this.productsService
                 .GetByStringId(id)
                 .ProjectTo<ProductViewModel>()
                 .SingleOrDefault();

            var item = this.itemsService.GetAll().SingleOrDefault(i => i.ProductId == product.ProductId);

            if (product == null)
            {
                return this.View("Error");
            }

            var productVM = new ProductFormViewModel()
            {
                Product = product
            };
        
              this.mapper.Map(item, productVM.Product);
        
              return this.View(productVM);
        }

        public ActionResult AddToWishlist(string id)
        {
            if (id == null)
            {
                return this.View("Error");
            }

          //  id = id.ToUpper();
            var product = this.productsService.GetByStringId(id).SingleOrDefault();
            var customerId = this.authProvider.CurrentUserId;
            var customer = this.usersService.GetAll().SingleOrDefault(u => u.Id == customerId);

            if (product == null)
            {
                return this.View("Error");
            }

            var wish = new WishlistItem()
            {
                Product = product,
                Customer = customer
            };

            this.wishesService.Add(wish);


            //make this return some http code so that you can make a modal success/fail popup ;)   
            return RedirectToAction("Index");
        }
    }
}