using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bulgarian_Apparel.Common;
using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Services;
using Bulgarian_Apparel.Services.Contracts;
using Bulgarian_Apparel.Web.Areas.Admin.Models;
using Bulgarian_Apparel.Web.Infrastructure;
using Bulgarian_Apparel.Web.Infrastructure.Validation;
using Bulgarian_Apparel.Web.Models.Common;
using Bulgarian_Apparel.Web.Models.Products;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Bulgarian_Apparel.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductManagementController : Controller
    {
        private readonly IProductsService productsService;
        private readonly IItemsService itemsService;
        private readonly ISizesService sizesService;
        private readonly IColorsService colorsService;
        private readonly ICategoriesService categoriesService;
        private readonly IMapper mapper;

        public ProductManagementController(IProductsService productsService, IItemsService itemsService, ISizesService sizesService, IColorsService colorsService, ICategoriesService categoriesService, IMapper mapper)
        {
            this.productsService = productsService ?? throw new ArgumentNullException(GlobalConstants.productsServiceCheck, nameof(productsService));
            this.itemsService = itemsService ?? throw new ArgumentNullException(GlobalConstants.itemsServiceCheck, nameof(itemsService));
            this.sizesService = sizesService ?? throw new ArgumentNullException(GlobalConstants.sizesServiceCheck, nameof(sizesService));
            this.colorsService = colorsService ?? throw new ArgumentNullException(GlobalConstants.colorsServiceCheck, nameof(colorsService));
            this.categoriesService = categoriesService ?? throw new ArgumentNullException(GlobalConstants.categoriesServiceCheck, nameof(categoriesService));
            this.mapper = mapper ?? throw new ArgumentNullException(GlobalConstants.mapperCheck, nameof(mapper));
        }
        
        // GET: Admin/ProductManagement
        public ActionResult Index()
        {
            return this.View();
        }

        [Authorize(Roles = "Admin")]
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


            var newProductAddVM = new AddProductViewModel()
            {
                Sizes = listSizes,
                Colors = listColors,
                Categories = categories
            };


            return this.View(newProductAddVM);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateModel]
        public ActionResult AddProduct(AddProductViewModel newProduct)
        {


            var colors = new List<Color>();
            foreach (var color in newProduct.SelectedColors)
            {
                if (!string.IsNullOrWhiteSpace(color))
                {
                    colors.Add(this.colorsService.GetColorForStringGuid(color));
                }
              
            }

            var sizes = new List<Size>();
            foreach (var size in newProduct.SelectedSizes)
            {
                if (!string.IsNullOrWhiteSpace(size))
                {
                    sizes.Add(this.sizesService.GetSizeForStringGuid(size));
                }
            }

            var itemdbModel = new Item()
            {
                Price = newProduct.Price,
                Sizes = sizes,
                Colors = colors
            };


            var category = this.categoriesService.CategoryByStringId(newProduct.CategoriesId).Single();
            var productdbModel = new Product()
            {
                Name = newProduct.Name,
                Description = newProduct.Description,
                Stock = newProduct.Stock,
                Supplier = newProduct.Supplier,
                Hot = false,
                Category = category,
                Sex = newProduct.Sex
            };

            itemdbModel.ProductId = productdbModel.Id;
            productdbModel.ItemId = itemdbModel.Id;




           foreach (var image in newProduct.Files)
           {
                var imageToAdd = new Image()
                {
                    FileName = Path.GetFileName(image.FileName),
                    Resource = "/Content/Products/" + image.FileName
                };

                var directoryLocation = Server.MapPath("/Content/Products/") + imageToAdd.FileName;
                image.SaveAs(directoryLocation);
                productdbModel.Images.Add(imageToAdd);
           }

            this.productsService.Add(productdbModel);
            this.itemsService.Add(itemdbModel);

            return this.RedirectToAction("ViewProduct", "Products", new { area = "", id = productdbModel.Id });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult RemoveProduct()
        {
            var products = this.productsService.GetAll().ProjectTo<DeleteProductViewModel>().ToList();


            return this.View(products);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult RemoveProductById(string Id)
        {
            var product = this.productsService.GetByStringId(Id).SingleOrDefault() ?? null;

            if(product == null)
            {
                Response.StatusCode = 400;
                return Content("Product with such id not found in db");
            }

            return this.Content(Id);
        }
            
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateProduct()
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


            var updateInformation = new UpdateProductViewModel()
            {
                Sizes = listSizes,
                Colors = listColors,
                Categories = categories
            };

            var updateTemplate = new UpdateProductInformationViewModel()
            {
                ProductInformation = new ProductInformationViewModel(),
                UpdateProduct = updateInformation
            };

            return this.View(updateTemplate);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult GetProductToUpdateByGuid(string id)
        {
            //check if product exists in db
            Guid productId = Guid.Parse(id);
            var product = this.productsService.GetAll().Single(p => p.Id == productId);
            var item = this.itemsService.GetAll().Single(i => i.ProductId == productId);
            var productInformation = this.mapper.Map<ProductInformationViewModel>(product);
            this.mapper.Map(item, productInformation);


            //Refractor this into a method - CreateForm()
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
            //

            var updateInformation = new UpdateProductViewModel()
            {
                Sizes = listSizes,
                Colors = listColors,
                Categories = categories
            };

            var updateTemplate = new UpdateProductInformationViewModel()
            {
                ProductInformation = productInformation,
                UpdateProduct = updateInformation
            };

            return this.PartialView("_ProductInformation", updateTemplate);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProduct(UpdateProductInformationViewModel updateProductViewModel)
        {
            var colors = new List<Color>();
            foreach (var color in updateProductViewModel.UpdateProduct.SelectedColors)
            {
                if (!updateProductViewModel.ProductInformation.Colors.Contains(color) && !string.IsNullOrWhiteSpace(color))
                {
                    colors.Add(this.colorsService.GetColorForStringGuid(color));
                }
               
            }

            var sizes = new List<Size>();
            foreach (var size in updateProductViewModel.UpdateProduct.SelectedSizes)
            {
                if (!updateProductViewModel.ProductInformation.Sizes.Contains(size) && !string.IsNullOrWhiteSpace(size))
                {
                    sizes.Add(this.sizesService.GetSizeForStringGuid(size));
                }
            }

            var productId = IdProccessor.GetGuidForStringId(updateProductViewModel.ProductInformation.Id);
            var itemDbModel = this.itemsService.GetAll().Single(i => i.ProductId == productId);

            itemDbModel.Price = Double.IsNaN(updateProductViewModel.UpdateProduct.Price) ? updateProductViewModel.ProductInformation.Price : updateProductViewModel.UpdateProduct.Price;
            itemDbModel.Sizes = sizes;
            itemDbModel.Colors = colors;
            
            var category = this.categoriesService.CategoryByStringId(updateProductViewModel.UpdateProduct.CategoriesId).Single() ?? updateProductViewModel.ProductInformation.Category;
            var productDbModel = new Product()
            {
                Name = updateProductViewModel.UpdateProduct.Name ?? updateProductViewModel.ProductInformation.Name,
                Description = updateProductViewModel.UpdateProduct.Description ?? updateProductViewModel.ProductInformation.Description,
                Stock = Double.IsNaN(updateProductViewModel.UpdateProduct.Stock) ? updateProductViewModel.ProductInformation.Stock : updateProductViewModel.UpdateProduct.Stock,
                Supplier = updateProductViewModel.UpdateProduct.Supplier ?? updateProductViewModel.ProductInformation.Supplier,
                Hot = false,
                Category = category,
                Sex = updateProductViewModel.UpdateProduct.Sex ?? updateProductViewModel.ProductInformation.Sex
            };



            foreach (var image in updateProductViewModel.UpdateProduct.Files)
            {
                //Check to see if current products images info contains the UPLOADED Images filename
                // TO DO: This check isn't sufficient enough, make a more durable one
                if (!updateProductViewModel.ProductInformation.Images.ToList().Any(im => im.Contains(image.FileName)) && image != null)
                {
                    var imageToAdd = new Image()
                    {
                        FileName = Path.GetFileName(image.FileName),
                        Resource = "/Content/Products/" + image.FileName
                    };

                    var directoryLocation = Server.MapPath("/Content/Products") + imageToAdd.FileName;
                    image.SaveAs(directoryLocation);
                    productDbModel.Images.Add(imageToAdd);
                }
            }

            this.productsService.Update(productDbModel);
            this.itemsService.Update(itemDbModel);

            return this.RedirectToAction("ViewProduct", "Products", new { area = "", id = productDbModel.Id });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                var all = this.productsService.GetAll().AsQueryable().ProjectTo<DeleteProductViewModel>().ToList();

                return this.PartialView("~/Areas/Admin/Views/Shared/_ProductsResult.cshtml", all);
            }
                
            var result = this.productsService
                .GetAll()
                .Where(product => product.Id.ToString().ToLower().Contains(query.ToLower()))
                .ProjectTo<DeleteProductViewModel>()
                .ToList();

            return this.PartialView("~/Areas/Admin/Views/Shared/_ProductsResult.cshtml", result);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult SearchById(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new HttpNotFoundResult();
            }

            var result = this.productsService
                .GetAll()
                .SingleOrDefault(pro => pro.Id.ToString().ToLower().Contains(query.ToLower()));
            var product = mapper.Map<AddProductViewModel>(result);

            return this.PartialView("_ProductInformationResult", product);
        }
    }
}