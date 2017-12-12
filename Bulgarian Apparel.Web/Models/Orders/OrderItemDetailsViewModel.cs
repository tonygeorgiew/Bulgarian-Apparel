using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Web.Infrastructure;
using Bulgarian_Apparel.Web.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace Bulgarian_Apparel.Web.Models.Orders
{
    public class OrderItemDetailsViewModel : IMapFrom<OrderItem>, IHaveCustomMappings
    {
        public Guid OrderId { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set;  }
        public string Color { get; set; }
        public string Brand { get; set; }           
        public string ShippingAddress { get; set; }
        public DateTime OrderedOn { get; set; }
        public double Price { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<OrderItem, OrderItemDetailsViewModel>()
                  .ForMember(oi => oi.OrderId, cfg => cfg.MapFrom(orderItem => orderItem.Order.Id))
                  .ForMember(oi => oi.ShippingAddress, cfg => cfg.MapFrom(orderItem => orderItem.Address))
                  .ForMember(oi => oi.Size, cfg => cfg.MapFrom(orderItem => orderItem.ProductSize))
                  .ForMember(oi => oi.Price, cfg => cfg.MapFrom(orderItem => orderItem.Payment))
                  .ForMember(oi => oi.Color, cfg => cfg.MapFrom(orderItem => orderItem.ProductColor))
                  .ForMember(oi => oi.Brand, cfg => cfg.MapFrom(orderItem => orderItem.Product.Supplier))
                  .ForMember(oi => oi.ProductName, cfg => cfg.MapFrom(orderItem => orderItem.Product.Name));
        }
    }
}