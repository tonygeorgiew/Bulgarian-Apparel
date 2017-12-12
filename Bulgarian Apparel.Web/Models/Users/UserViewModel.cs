using AutoMapper;
using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Web.Infrastructure;
using Bulgarian_Apparel.Web.Models.Home;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bulgarian_Apparel.Web.Areas.Admin.Models
{
    public class UserViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }
            
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public ICollection<Order> Orders { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<User, UserViewModel>()
                .ForMember(userViewModel => userViewModel.UserName, cfg => cfg.MapFrom(user => user.UserName))
                .ForMember(userViewModel => userViewModel.Email, cfg => cfg.MapFrom(user => user.Email))
                .ForMember(userViewModel => userViewModel.Orders, cfg => cfg.MapFrom(user => user.Orders));
        }
    }
}