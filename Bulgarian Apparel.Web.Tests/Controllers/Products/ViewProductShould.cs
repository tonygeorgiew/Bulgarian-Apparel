using AutoMapper;
using Bulgarian_Apparel.Auth;
using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Services;
using Bulgarian_Apparel.Services.Contracts;
using Bulgarian_Apparel.Services.Data.Contracts;
using Bulgarian_Apparel.Web.Controllers;
using Bulgarian_Apparel.Web.Models.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.JustMock;
using TestStack.FluentMVCTesting;

namespace Bulgarian_Apparel.Web.Tests.Controllers.Products
{
    [TestClass]
    public class ViewProductShould
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
        public void ReturnErrorPage_WhenPassedIdIsNull()
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
            var controller = new ProductsController(
            usersServiceMock, productsServiceMock, itemsServiceMock,
            wishlistServiceMock, sizesServiceMock, colorsServiceMock,
            categoriesServiceMock, mapperMock, authProviderMock);

            // Act & Assert
            controller
                .WithCallTo(c => c.ViewProduct(null))
                .ShouldRenderView("Error");

            Mapper.Reset();
        }

        [TestMethod]
        public void ReturnErrorPage_WhenPassedIdProductIsNotFound()
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
            var getGuid = Guid.NewGuid().ToString();

            var controller = new ProductsController(
            usersServiceMock, productsServiceMock, itemsServiceMock,
            wishlistServiceMock, sizesServiceMock, colorsServiceMock,
            categoriesServiceMock, mapperMock, authProviderMock);

            // Act & Assert
            controller
                .WithCallTo(c => c.ViewProduct(getGuid))
                .ShouldRenderView("Error");

            Mapper.Reset();
        }

        [TestMethod]
        public void ReturnDefaultView_WithCorrectlyPassedId()
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
            var productId = Guid.NewGuid();
            var product = new Product
            {
                Id = productId
            };


            var controller = new ProductsController(
            usersServiceMock, productsServiceMock, itemsServiceMock,
            wishlistServiceMock, sizesServiceMock, colorsServiceMock,
            categoriesServiceMock, mapperMock, authProviderMock);
            var list = new List<Product>() { product };
            Mock.Arrange(() => productsServiceMock.GetByStringId(productId.ToString())).Returns(list.AsQueryable());

            // Act
            controller.ViewProduct(productId.ToString());

            // Assert
            controller
                .WithCallTo(c => c.ViewProduct(productId.ToString()))
                .ShouldRenderDefaultView();

            Mapper.Reset();
        }
    }
}
