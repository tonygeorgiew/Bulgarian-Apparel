using AutoMapper;
using Bulgarian_Apparel.Auth;
using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Services;
using Bulgarian_Apparel.Services.Contracts;
using Bulgarian_Apparel.Services.Data.Contracts;
using Bulgarian_Apparel.Web.Controllers;
using Bulgarian_Apparel.Web.Models.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using TestStack.FluentMVCTesting;

namespace Bulgarian_Apparel.Web.Tests.Controllers.Products
{
    [TestClass]
    public class IndexShould
    {

        [TestMethod]
        public void RenderDefaultView()
        {
            // Arrange
            var usersServiceMock = Mock.Create<IUsersService>();
            var productsServiceMock = Mock.Create<IProductsService>();
            var itemsServiceMock = Mock.Create<IItemsService>();
            var wishlistServiceMock = Mock.Create<IWishlistsService>();
            var sizesServiceMock = Mock.Create<ISizesService>();
            var colorsServiceMock = Mock.Create<IColorsService>();
            var categoriesServiceMock = Mock.Create<ICategoriesService>();
            var authProviderMock = Mock.Create<IAuthProvider>();
            var mapperMock = Mock.Create<IMapper>();

            // Act
            var controller = new ProductsController(
            usersServiceMock, productsServiceMock, itemsServiceMock,
            wishlistServiceMock, sizesServiceMock, colorsServiceMock,
            categoriesServiceMock, mapperMock, authProviderMock);

            // Assert
            controller
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView();

        }

        [TestMethod]
        public void CallTo_ProductsService_GetAll()
        {
            // Arrange
            var usersServiceMock = Mock.Create<IUsersService>();
            var productsServiceMock = Mock.Create<IProductsService>();
            var itemsServiceMock = Mock.Create<IItemsService>();
            var wishlistServiceMock = Mock.Create<IWishlistsService>();
            var sizesServiceMock = Mock.Create<ISizesService>();
            var colorsServiceMock = Mock.Create<IColorsService>();
            var categoriesServiceMock = Mock.Create<ICategoriesService>();
            var authProviderMock = Mock.Create<IAuthProvider>();
            var mapperMock = Mock.Create<IMapper>();

            // Act
            var controller = new ProductsController(
            usersServiceMock, productsServiceMock, itemsServiceMock,
            wishlistServiceMock, sizesServiceMock, colorsServiceMock,
            categoriesServiceMock, mapperMock, authProviderMock);

            controller.Index();

            // Assert
            Mock.Assert(() => productsServiceMock.GetAll(), Occurs.Once());
        }
    }
}
