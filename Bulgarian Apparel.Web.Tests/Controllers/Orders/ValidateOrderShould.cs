using AutoMapper;
using Bulgarian_Apparel.Auth;
using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Services;
using Bulgarian_Apparel.Services.Contracts;
using Bulgarian_Apparel.Web.Controllers;
using Bulgarian_Apparel.Web.Models.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;
using TestStack.FluentMVCTesting;

namespace Bulgarian_Apparel.Web.Tests.Controllers.Orders
{
    [TestClass]
    public class ValidateOrderShould
    {
        [TestMethod]
        public void ThrowIfModelStateIsInvalid()
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
            var form = new CheckoutFormViewModel();


            //controller.ValidateOrder(form);

            //Act & Assert
            Mock.Arrange(() => controller.ValidateOrder(form)).OccursOnce();

            //Mock.ThrowIfModelStateIsInvalid();
        }
    }
}
