using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Web.Infrastructure;
using Bulgarian_Apparel.Web.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace Bulgarian_Apparel.Web.Models.Products
{
    public class HotProductViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public Guid Id { get; set; }
        public string[] Image { get; set; }
        public string Description { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Product, HotProductViewModel>()
                .ForMember(productVM => productVM.Id, cfg => cfg.MapFrom(src => src.Id))
                .ForMember(productVM => productVM.Description, cfg => cfg.MapFrom(src => src.Description));
            configuration.CreateMap<Product, HotProductViewModel>()
                .ForMember(productVM => productVM.Image,
                cfg => cfg
                .MapFrom(src => src.Images.Select(child => child.Resource)
                .ToArray()));
        }
    }
}