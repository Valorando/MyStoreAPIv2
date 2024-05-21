using GetProductsService.Controllers;
using GetProductsService.Interfaces;
using GetProductsService.Models;
using MyStoreGatewayAPI.Controllers;
using MyStoreGatewayAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Logging;

namespace ControllersTest
{
    public class ProductsControllerTest
    {
        private readonly ProductsController _productsController;
        private readonly Mock<IProductService> _productServiceMock;
        private readonly Mock<ILogger<ProductsController>> _loggerMock;

        public ProductsControllerTest()
        {
            _productServiceMock = new Mock<IProductService>();
            _loggerMock = new Mock<ILogger<ProductsController>>();
            _productsController = new ProductsController(_productServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task ProductControllerReturnValueCheck()
        {
            var expectedProducts = new List<ProductModel>
            {
                new ProductModel { ProductId = 111, Name = "Test1", Amount = 10, Price = "100$" },
                new ProductModel { ProductId = 222, Name = "Test2", Amount = 20, Price = "200$" },
                new ProductModel { ProductId = 333, Name = "Test3", Amount = 30, Price = "300$" },
                new ProductModel { ProductId = 444, Name = "Test4", Amount = 40, Price = "400$" },
                new ProductModel { ProductId = 555, Name = "Test5", Amount = 50, Price = "500$" },
                new ProductModel { ProductId = 666, Name = "Test6", Amount = 60, Price = "600$" }
            };

            _productServiceMock.Setup(service => service.GetAllProducts()).ReturnsAsync(expectedProducts);

            var actionResult = await _productsController.Get();
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            var actualProducts = Assert.IsAssignableFrom<List<ProductModel>>(okObjectResult.Value);

            Assert.Equal(expectedProducts.Count, actualProducts.Count);

            for (int i = 0; i < expectedProducts.Count; i++)
            {
                Assert.Equal(expectedProducts[i].ProductId, actualProducts[i].ProductId);
                Assert.Equal(expectedProducts[i].Name, actualProducts[i].Name);
                Assert.Equal(expectedProducts[i].Amount, actualProducts[i].Amount);
                Assert.Equal(expectedProducts[i].Price, actualProducts[i].Price);
            }
        }
    }

    public class GetProductsControllerTest
    {
        private readonly GetProductsController _getProductsController;
        private readonly Mock<IGetProductsService> _getProductsServiceMock;
        private readonly Mock<ILogger<GetProductsController>> _getProductsLoggerMock;

        public GetProductsControllerTest()
        {
            _getProductsServiceMock = new Mock<IGetProductsService>();
            _getProductsLoggerMock = new Mock<ILogger<GetProductsController>>();
            _getProductsController = new GetProductsController(_getProductsServiceMock.Object, _getProductsLoggerMock.Object);
        }

        [Fact]
        public async Task GetProductsControllerReturnValueCheck()
        {
            var expectedProductsJson = "[{\"productId\":111,\"name\":\"Test1\",\"amount\":10,\"price\":\"100$\"}," +
                                       "{\"productId\":222,\"name\":\"Test2\",\"amount\":20,\"price\":\"200$\"}," +
                                       "{\"productId\":333,\"name\":\"Test3\",\"amount\":30,\"price\":\"300$\"}," +
                                       "{\"productId\":444,\"name\":\"Test4\",\"amount\":40,\"price\":\"400$\"}," +
                                       "{\"productId\":555,\"name\":\"Test5\",\"amount\":50,\"price\":\"500$\"}," +
                                       "{\"productId\":666,\"name\":\"Test6\",\"amount\":60,\"price\":\"600$\"}]";

            _getProductsServiceMock.Setup(service => service.GetAllProductsRequest()).ReturnsAsync(expectedProductsJson);

            var actionResult = await _getProductsController.Get();
            var okObjectResult = actionResult.Result as OkObjectResult;

            Assert.NotNull(okObjectResult);
            var actualProductsJson = Assert.IsType<string>(okObjectResult.Value);

            Assert.Equal(expectedProductsJson, actualProductsJson);
        }
    }
}
