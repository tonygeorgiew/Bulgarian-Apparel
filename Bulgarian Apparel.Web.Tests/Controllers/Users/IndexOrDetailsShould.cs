namespace Bulgarian_Apparel.Web.Tests.Controllers.Users
{
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
    using System.Web.Mvc;
    using Telerik.JustMock;
    using TestStack.FluentMVCTesting;

    [TestClass]
    public class IndexOrDetailsShould
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
            var usersServiceMock = Mock.Create<IUsersService>();
            var productsServiceMock = Mock.Create<IProductsService>();
            var wishlistServiceMock = Mock.Create<IWishlistsService>();

            // Act
            var controller = new UsersController(
                usersServiceMock, productsServiceMock, wishlistServiceMock);

            // Assert
            controller
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void CallTo_UsersService_GetAll()
        {
            // Arrange
            var usersServiceMock = Mock.Create<IUsersService>();
            var productsServiceMock = Mock.Create<IProductsService>();
            var wishlistServiceMock = Mock.Create<IWishlistsService>();

            // Act
            var controller = new UsersController(
                usersServiceMock, productsServiceMock, wishlistServiceMock);

            controller.Index();

            // Assert
            Mock.Assert(() => usersServiceMock.GetAll(), Occurs.Once());
        }

        [TestMethod]
        public void ReturnErrorPage_WhenPassedIdIsNull()
        {
            // Arrange
            var usersServiceMock = Mock.Create<IUsersService>();
            var productsServiceMock = Mock.Create<IProductsService>();
            var wishlistServiceMock = Mock.Create<IWishlistsService>();
            var controller = new UsersController(
                usersServiceMock, productsServiceMock, wishlistServiceMock);

            // Act & Assert
            controller
               .WithCallTo(c => c.Details(null))
               .ShouldRenderView("Error");
        }

        [TestMethod]
        public void ReturnErrorPage_WhenUserIsNull()
        {
            // Arrange
            var usersServiceMock = Mock.Create<IUsersService>();
            var productsServiceMock = Mock.Create<IProductsService>();
            var wishlistServiceMock = Mock.Create<IWishlistsService>();
            var id = Guid.NewGuid();
            var controller = new UsersController(
                usersServiceMock, productsServiceMock, wishlistServiceMock);


            // Act & Assert
            controller
               .WithCallTo(c => c.Details(id.ToString()))
               .ShouldRenderView("Error");
        }

        [TestMethod]
        public void ReturnDefaultView_WhenUserIsNotNull()
        {
            // Arrange
            var usersServiceMock = Mock.Create<IUsersService>();
            var productsServiceMock = Mock.Create<IProductsService>();
            var wishlistServiceMock = Mock.Create<IWishlistsService>();
            var id = Guid.NewGuid();
            var controller = new UsersController(
                usersServiceMock, productsServiceMock, wishlistServiceMock);
            var ulist = new List<User>() { new User { } };

            Mock.Arrange(() => usersServiceMock.GetUserById(id.ToString())).Returns(ulist.AsQueryable).MustBeCalled();


            // Act & Assert
            controller
               .WithCallTo(c => c.Details(id.ToString()))
               .ShouldRenderDefaultView();
        }
    }
}
