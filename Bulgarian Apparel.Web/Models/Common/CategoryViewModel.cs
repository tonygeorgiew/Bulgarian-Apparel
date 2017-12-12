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
    public class CategoryViewModel : IMapFrom<Category>, IHaveCustomMappings
    { 
        public string CategoryName { get; set; }

        public string SuperCategoryName { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Category, CategoryViewModel>()
                .ForMember(catVM => catVM.CategoryName, cfg => cfg.MapFrom(cat => (cat.Name.Contains(" ") ? cat.Name.Replace(' ', '-') : cat.Name)))
                .ForMember(catVM => catVM.SuperCategoryName, cfg => cfg.MapFrom(cat => cat.SuperCategoryName ?? ""));
        }
    }
}