using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Web.Infrastructure;
using Bulgarian_Apparel.Web.Models.Home;
using System;
using AutoMapper;
using System.Collections.Generic;

namespace Bulgarian_Apparel.Web.Models.Products
{
    public class ProductViewModel : IHaveCustomMappings
    {
        public string Name { get; set; }
        public string Supplier { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public IList<Image> ImageResources { get; set; }
        public IList<Size> Sizes { get; set; }
        public IList<Color> Colors { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {

            //Problema e v tova che trqbva da se mapne cqlata koleciq(a imenno path na image, a ne celiq image) a ne prosto propertyto
            configuration.CreateMap<Product, ProductViewModel>()
                .ForMember(productVM => productVM.ImageResources, cfg => cfg.MapFrom(product => product.Images));
            configuration.CreateMap<Item, ProductViewModel>()
                .ForMember(productVM => productVM.Price, cfg => cfg.MapFrom(item => item.Price))
                .ForMember(productVM => productVM.Sizes, cfg => cfg.MapFrom(item => item.Sizes))
                .ForMember(productVM => productVM.Colors, cfg => cfg.MapFrom(item => item.Colors));
        }
    }
}