﻿using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Web.Infrastructure;
using Bulgarian_Apparel.Web.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Bulgarian_Apparel.Web.Areas.Admin.Models
{
    public class AddProductViewModel : IMapFrom<Product>, IMapFrom<Item>, IHaveCustomMappings
    {
        public AddProductViewModel()
        {
            Files = new List<HttpPostedFileBase>();
        }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        [Display(Name = "Product Brand")]
        public string Supplier { get; set; }

        [Required]
        [Display(Name = "Product Image Resources")]
        public List<HttpPostedFileBase> Files { get; set; }

        [Required]
        [Display(Name = "Product is available in what colors (Ctrl + left mouse click for multiple selection)")]
        public IEnumerable<string> SelectedColors { get; set; }
        public IEnumerable<SelectListItem> Colors { get; set; }

        [Required]
        [Display(Name = "Product is available in what sizes (Ctrl + left mouse click for multiple selection)")]
        public IEnumerable<string> SelectedSizes { get; set; }
        public IEnumerable<SelectListItem> Sizes { get; set; }

        [Display(Name = "Choose a single category")]
        public IList<Category> Categories { get; set; }

        [Required]
        public string CategoriesId { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Stock { get; set; }

        public IList<string> SelectedSex { get; set; }

        [Required]
        public string Sex { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
           //configuration.CreateMap<Product, AddProductViewModel>()
           //    .ForMember(productVM => productVM.Images,
           //    cfg => cfg
           //    .MapFrom(src => src.Images.Select(child => child.Resource)
           //    .ToArray()));

            configuration.CreateMap<Item, AddProductViewModel>()
                .ForMember(productViewModel => productViewModel.Sizes, cfg => cfg.MapFrom(item => item.Sizes))
                .ForMember(productViewModel => productViewModel.Colors, cfg => cfg.MapFrom(item => item.Colors));
        }
    }
}