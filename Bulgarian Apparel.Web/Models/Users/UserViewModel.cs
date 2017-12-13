using AutoMapper;
using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Web.Infrastructure;
using Bulgarian_Apparel.Web.Models.Home;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bulgarian_Apparel.Web.Models.Users
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

        public ICollection<WishlistItem> Wishlist { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<User, UserViewModel>()
                .ForMember(userViewModel => userViewModel.UserName, cfg => cfg.MapFrom(user => user.UserName))
                .ForMember(userViewModel => userViewModel.Email, cfg => cfg.MapFrom(user => user.Email))
                .ForMember(userViewModel => userViewModel.Wishlist, cfg => cfg.MapFrom(user => user.WishlistItems));
        }
    }
}