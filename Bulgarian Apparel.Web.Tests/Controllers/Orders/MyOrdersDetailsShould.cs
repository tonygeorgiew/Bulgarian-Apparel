using AutoMapper;
using Bulgarian_Apparel.Auth;
using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Services.Contracts;
using Bulgarian_Apparel.Web.Controllers;
using Bulgarian_Apparel.Web.Models.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.JustMock;
using TestStack.FluentMVCTesting;

namespace Bulgarian_Apparel.Web.Tests.Controllers.Orders
{
    [TestClass]
    public class MyOrdersDetailsShould
    {

        [TestInitialize]
        public void InitAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ProductViewModel>();
                cfg.CreateMap<Category, CategoryViewModel>();
                cfg.CreateMap<Item, ProductViewModel>();
            });
        }

        

        [TestMethod]
        public void RenderDefaultView()
        {
            
            // Arrange
            var productsServiceMock = Mock.Create<IProductsService>();
            var ordersServiceMock = Mock.Create<IOrdersService>();
            var orderItemsServiceMock = Mock.Create<IOrdersItemsService>();
            var shoppingCartServiceMock = Mock.Create<IShoppingCartService>();
            var paymentTypesService = Mock.Create<IPaymentTypesService>();
            var authProviderMock = Mock.Create<IAuthProvider>();
            var usersServiceMock = Mock.Create<IUsersService>();
            var mapperMock = Mock.Create<IMapper>();
            OrdersController controller = new OrdersController(
            productsServiceMock, ordersServiceMock, orderItemsServiceMock,
            shoppingCartServiceMock, usersServiceMock, paymentTypesService,
            mapperMock, authProviderMock);
            var id = Guid.NewGuid();
            var list = new List<OrderItem>() { new OrderItem() { Id = id} };


            Mock.Arrange(() => orderItemsServiceMock.GetByCustomerId(id.ToString())).Returns(list.AsQueryable).MustBeCalled();

            //Act & Assert
            controller
              .WithCallTo(c => c.MyOrdersDetails())
              .ShouldRenderDefaultView();
        }
    }
}
