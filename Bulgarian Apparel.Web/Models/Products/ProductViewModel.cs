using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Web.Infrastructure;
using Bulgarian_Apparel.Web.Models.Home;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Bulgarian_Apparel.Web.Models.Products
{
    public class ProductViewModel : IHaveCustomMappings
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Supplier { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public IList<string> ImageResources { get; set; }
        public IList<Size> Sizes { get; set; }
        public IList<Color> Colors { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            //Problema e v tova che trqbva da se mapne cqlata koleciq(a celiq image, a ne samo image path) a ne prosto propertyto
            configuration.CreateMap<Product, ProductViewModel>()
                .ForMember(productVM => productVM.ImageResources,
                           cfg => cfg.MapFrom(
                               src => src.Images.Select(child => child.Resource).ToArray()));
            configuration.CreateMap<Item, ProductViewModel>()
                .ForMember(productVM => productVM.Price, cfg => cfg.MapFrom(item => item.Price))
                .ForMember(productVM => productVM.Sizes, cfg => cfg.MapFrom(item => item.Sizes))
                .ForMember(productVM => productVM.Colors, cfg => cfg.MapFrom(item => item.Colors));
        }
    }
}