namespace Bulgarian_Apparel.Web.Tests.Controllers.Products
{
    using Bulgarian_Apparel.Services;
    using Bulgarian_Apparel.Services.Contracts;
    using Bulgarian_Apparel.Services.Data.Contracts;
    using Bulgarian_Apparel.Web.Controllers;
    using System;
    using AutoMapper;
    using Moq;
    using Bulgarian_Apparel.Auth;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ConstructorShould
    {
        [TestMethod]
        public void ThrowsWhenOneServiceIsNull()
        {
            // Arrange
            var usersServiceMock = new Mock<IUsersService>();
         // var productsServiceMock = new Mock<IProductsService>();
            var itemsServiceMock = new Mock<IItemsService>();
            var wishlistServiceMock = new Mock<IWishlistsService>();
            var sizesServiceMock = new Mock<ISizesService>();
            var colorsServiceMock = new Mock<IColorsService>();
            var categoriesServiceMock = new Mock<ICategoriesService>();
            var authProviderMock = new Mock<IAuthProvider>();
            var mapperMock = new Mock<IMapper>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new ProductsController(
             usersServiceMock.Object, null, itemsServiceMock.Object,
             wishlistServiceMock.Object, sizesServiceMock.Object, colorsServiceMock.Object,
             categoriesServiceMock.Object, mapperMock.Object, authProviderMock.Object));
        }

        [TestMethod]
        public void ThrowsWhenMapperIsNull()
            {
            // Arrange
            var usersServiceMock = new Mock<IUsersService>();
            var productsServiceMock = new Mock<IProductsService>();
            var itemsServiceMock = new Mock<IItemsService>();
            var wishlistServiceMock = new Mock<IWishlistsService>();
            var sizesServiceMock = new Mock<ISizesService>();
            var colorsServiceMock = new Mock<IColorsService>();
            var categoriesServiceMock = new Mock<ICategoriesService>();
            var authProviderMock = new Mock<IAuthProvider>();
            // var mapperMock = new Mock<IMapper>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new ProductsController(
            usersServiceMock.Object, productsServiceMock.Object, itemsServiceMock.Object,
            wishlistServiceMock.Object, sizesServiceMock.Object, colorsServiceMock.Object,
            categoriesServiceMock.Object, null, authProviderMock.Object));
        }

        [TestMethod]
        public void ThrowsWhenAuthProviderIsNull()
        {
            // Arrange
            var usersServiceMock = new Mock<IUsersService>();
            var productsServiceMock = new Mock<IProductsService>();
            var itemsServiceMock = new Mock<IItemsService>();
            var wishlistServiceMock = new Mock<IWishlistsService>();
            var sizesServiceMock = new Mock<ISizesService>();
            var colorsServiceMock = new Mock<IColorsService>();
            var categoriesServiceMock = new Mock<ICategoriesService>();
         // var authProviderMock = new Mock<IAuthProvider>();
            var mapperMock = new Mock<IMapper>();

            // Act & Assert
             Assert.ThrowsException<ArgumentNullException>(() => new ProductsController(
             usersServiceMock.Object, productsServiceMock.Object, itemsServiceMock.Object,
             wishlistServiceMock.Object, sizesServiceMock.Object, colorsServiceMock.Object,
             categoriesServiceMock.Object, mapperMock.Object, null));
        }

        [TestMethod]
        public void SetPassedDataCorrectly()
        {
            // Arrange
            var usersServiceMock = new Mock<IUsersService>();
            var productsServiceMock = new Mock<IProductsService>();
            var itemsServiceMock = new Mock<IItemsService>();
            var wishlistServiceMock = new Mock<IWishlistsService>();
            var sizesServiceMock = new Mock<ISizesService>();
            var colorsServiceMock = new Mock<IColorsService>();
            var categoriesServiceMock = new Mock<ICategoriesService>();
            var authProviderMock = new Mock<IAuthProvider>();
            var mapperMock = new Mock<IMapper>();

            // Act
            var controller = new ProductsController(
            usersServiceMock.Object, productsServiceMock.Object, itemsServiceMock.Object,
            wishlistServiceMock.Object, sizesServiceMock.Object, colorsServiceMock.Object,
            categoriesServiceMock.Object, mapperMock.Object, authProviderMock.Object);

            //Assert
            Assert.IsNotNull(controller);
            Assert.IsInstanceOfType(controller, typeof(ProductsController));
        }
    }
}
