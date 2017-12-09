﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Services;
using Bulgarian_Apparel.Services.Contracts;
using Bulgarian_Apparel.Web.Areas.Admin.Models;
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
                catalogueProduct.Category.CategoryName = !product.Category.Name.IsNullOrWhiteSpace() ? (product.Category.Name.Contains(" ") ? product.Category.Name.Replace(' ', '-') : product.Category.Name) : "";
                catalogueProduct.Category.SuperCategoryName = product.Category.SuperCategoryName ?? null;
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
    }
}