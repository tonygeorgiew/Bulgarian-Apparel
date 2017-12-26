using AutoMapper;
using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Web.Infrastructure;
using Bulgarian_Apparel.Web.Models.Home;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bulgarian_Apparel.Web.Areas.Admin.Models
{
    public class ProductInformationViewModel : IMapFrom<Product>, IMapFrom<Item>, IHaveCustomMappings
    {
        [Required]
        public string Id { get; set; }

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
        [Display(Name = "Product Image Resources in format(/Content/Products/filename.jpg)")]
        public IEnumerable<string> Images { get; set; }

        [Required]
        public IEnumerable<string> Colors{ get; set; }
        
        [Required]
        public IEnumerable<string> Sizes { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public string Sex { get; set; }


        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Product, ProductInformationViewModel>()
                .ForMember(productViewModel => productViewModel.Images,
                cfg => cfg
                .MapFrom(src => src.Images.Select(child => child.Resource)
                .ToArray()))
                .ForMember(productViewModel => productViewModel.Id, cfg => cfg.MapFrom(product => product.Id));

            configuration.CreateMap<Item, ProductInformationViewModel>()
                .ForMember(productViewModel => productViewModel.Sizes,
                 cfg => cfg
                .MapFrom(src => src.Sizes.Select(child => child.Name)
                .ToArray()))
                .ForMember(productViewModel => productViewModel.Colors,
                 cfg => cfg
                .MapFrom(src => src.Colors.Select(child => child.Name)
                .ToArray()));
        }
    }
}