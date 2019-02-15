using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ActionService;
using BusinessObjects;
using Mvc.Areas.Shop.Controllers;
using Mvc.Code;
using Mvc.Areas.Shop.Models;

namespace Mvc.Tests.Controllers
{
   
    // testClass for ShopController.
    // note: the pattern in each TestMethod is: AAA (Arrange, Act, Assert).
    
    [TestClass]
    public class ShopControllerTest
    {
        // the mock service

        Mock<IService> mockService;

        // initialize testing environment by setting up the mocked Service and their return values.
        
        [TestInitialize]
        public void InitializeMocks()
        {
            mockService = new Mock<IService>();

            // setup for getting a list of categories

            var categories = new List<Category> { new Category { CategoryId = 1, CategoryName = "test-category" } };
            mockService.Setup(s => s.GetCategories()).Returns(categories);

            // setup for getting a list of products

            var products = new List<Product> { new Product { ProductId = 1, ProductName = "test-product" } };
            mockService.Setup(s => s.GetProductsByCategory(It.IsAny<int>(),"unitprice asc")).Returns(products);

            // setup for getting a product

            var product = new Product { ProductId = 1, ProductName = "test-product", Category = new Category { CategoryName = "test-category" } };
            mockService.Setup(s => s.GetProduct(1)).Returns(product);

            // setup for searching for products

            mockService.Setup(s => s.SearchProducts("", 0, 5000, "unitprice asc")).Returns(products);
        }

        // helper. creates shop controller
        // this is a Factory Method

        ShopController CreateShopController()
        {
            // Note: this is where DI (Dependency Injection) takes place.
            // the service layer is injected (via the constructor) into the controller.

            return new ShopController(mockService.Object);
        }

        // tests Shopping page

        [TestMethod]
        public void ShoppingTest()
        {
            // Arrange
            var controller = CreateShopController();

            // Act
            var result = controller.Shopping() as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(result.ViewName, string.Empty);
        }

        // tests Products action method. Category and Products retrieval.

        [TestMethod]
        public void ProductsTest()
        {
            // Arrange 
            var controller = CreateShopController();

            // Act
            var result = controller.Products() as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(ProductsModel));

            Assert.AreEqual(result.ViewName, string.Empty);

            // check categories and sortable products returned
            var model = result.ViewData.Model as ProductsModel;

            Assert.AreEqual(1, model.Categories.Count());
            Assert.AreEqual(1, model.Products.List.Count());
        }

        // tests Product action method. single product retrieval.

        [TestMethod]
        public void ProductTest()
        {
            // Arrange
            var controller = CreateShopController();

            // Act
            var result = controller.Product(1) as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(ProductModel));

            // check product name

            Assert.AreEqual((result.ViewData.Model as ProductModel).ProductName, "test-product");
        }

        // test Product Search Action method. Returns sorted product list.
        // Note: this creates a Fake HttpContext which is necessary to access an [HttpPost] decorated action method.

        [TestMethod]
        public void SearchTest()
        {
            // Arrange
            var controller = CreateShopController();

            controller.SetFakeControllerContext();
            controller.Request.SetHttpMethodResult("POST");

            // Act
            var result = controller.Search() as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(SearchModel));

            // check products returned

            var model = result.ViewData.Model as SearchModel;
            Assert.AreEqual(1, model.Products.List.Count());
        }
    }
}


