using AutoMapper;
using Bulgarian_Apparel.Auth;
using Bulgarian_Apparel.Services;
using Bulgarian_Apparel.Services.Contracts;
using Bulgarian_Apparel.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulgarian_Apparel.Web.Tests.Controllers.Orders
{
    [TestClass]
    public class ConstructorShould
    {
        [TestMethod]
        public void ThrowsWhenOneServiceIsNull()
        {
            // Arrange
            var productsServiceMock = new Mock<IProductsService>();
            var ordersServiceMock = new Mock<IOrdersService>();
            var orderItemsServiceMock = new Mock<IOrdersItemsService>();
            var shoppingCartServiceMock = new Mock<IShoppingCartService>();
            var paymentTypesService = new Mock<IPaymentTypesService>();
            var usersServiceMock = new Mock<IUsersService>();
            var authProviderMock = new Mock<IAuthProvider>();
            var mapperMock = new Mock<IMapper>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new OrdersController(
            productsServiceMock.Object,ordersServiceMock.Object,orderItemsServiceMock.Object,
            null, usersServiceMock.Object, paymentTypesService.Object, mapperMock.Object, authProviderMock.Object));
        }

        [TestMethod]
        public void ThrowsWhenMapperIsNull()
        {
            // Arrange
            var productsServiceMock = new Mock<IProductsService>();
            var ordersServiceMock = new Mock<IOrdersService>();
            var orderItemsServiceMock = new Mock<IOrdersItemsService>();
            var shoppingCartServiceMock = new Mock<IShoppingCartService>();
            var paymentTypesService = new Mock<IPaymentTypesService>();
            var usersServiceMock = new Mock<IUsersService>();
            var authProviderMock = new Mock<IAuthProvider>();
            var mapperMock = new Mock<IMapper>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new OrdersController(
            productsServiceMock.Object, ordersServiceMock.Object, orderItemsServiceMock.Object,
            shoppingCartServiceMock.Object, usersServiceMock.Object, paymentTypesService.Object, null, authProviderMock.Object));
        }

        [TestMethod]
        public void SetsPassedData()
        {
            // Arrange
            var productsServiceMock = new Mock<IProductsService>();
            var ordersServiceMock = new Mock<IOrdersService>();
            var orderItemsServiceMock = new Mock<IOrdersItemsService>();
            var shoppingCartServiceMock = new Mock<IShoppingCartService>();
            var paymentTypesService = new Mock<IPaymentTypesService>();
            var usersServiceMock = new Mock<IUsersService>();
            var authProviderMock = new Mock<IAuthProvider>();
            var mapperMock = new Mock<IMapper>();

            // Act & Assert
            OrdersController controller = new OrdersController(
            productsServiceMock.Object, ordersServiceMock.Object, orderItemsServiceMock.Object,
            shoppingCartServiceMock.Object, usersServiceMock.Object, paymentTypesService.Object, 
            mapperMock.Object, authProviderMock.Object);


            Assert.IsNotNull(controller);
            Assert.IsInstanceOfType(controller, typeof(OrdersController));
        }
    }
}
