using Moq;
using Sofka.ProductInventory.Aplication;
using Sofka.ProductInventory.Core.Domain.Interfaces;
using Sofka.ProductInventory.Core.Dto;
using Sofka.ProductInventory.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofka.ProductInventory.AplicationTest
{
    public class ProductServiceTest
    {
        private readonly Mock<IBaseRepository<Product>> _productRepositoryMock;
        private readonly ProductService _productService;

        public ProductServiceTest()
        {
            _productRepositoryMock = new Mock<IBaseRepository<Product>>();
            _productService = new ProductService(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task Create_ValidProduct_CreatesProduct()
        {
            // Arrange
            var product = new Product();

            // Act
            await _productService.Create(product);

            // Assert
            _productRepositoryMock.Verify(x => x.AddAsync(product), Times.Once);
        }

        [Fact]
        public async Task Delete_ProductId_DeletesProduct()
        {
            // Arrange
            var productId = 1;

            // Act
            await _productService.Delete(productId);

            // Assert
            _productRepositoryMock.Verify(x => x.DeleteAsync(productId), Times.Once);
        }

        [Fact]
        public async Task GetProductById_ValidProductId_ReturnsProduct()
        {
            // Arrange
            var productId = 1;
            var expectedProduct = new Product { Id = productId };

            _productRepositoryMock.Setup(x => x.GetByIdAsync(productId)).ReturnsAsync(expectedProduct);

            // Act
            var result = await _productService.GetProductById(productId);

            // Assert
            Assert.Equal(expectedProduct, result);
            _productRepositoryMock.Verify(x => x.GetByIdAsync(productId), Times.Once);
        }

        [Fact]
        public async Task GetProducts_ReturnsListOfProducts()
        {
            // Arrange
            var expectedProducts = new List<Product> { new Product(), new Product(), new Product() };

            _productRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(expectedProducts);

            // Act
            var result = await _productService.GetProducts();

            // Assert
            Assert.Equal(expectedProducts, result);
            _productRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Update_ValidProduct_UpdatesProduct()
        {
            // Arrange
            var product = new Product();

            // Act
            await _productService.Update(product);

            // Assert
            _productRepositoryMock.Verify(x => x.UpdateAsync(product), Times.Once);
        }

        [Theory]
        [InlineData(10, 5, 1, 10, true)]   // Valid case
        [InlineData(10, 15, 1, 10, false)]  // Quantity exceeds Max
        [InlineData(10, 5, 15, 20, false)]  // Quantity below Min
        [InlineData(10, 5, 15, 5, false)]   // Quantity below Min and above Max
        public void ProductIsValidForBuy_ValidatesProduct(int inInventory, int quantity, int min, int max, bool expectedResult)
        {
            // Arrange
            var product = new Product { InInventory = inInventory, Min = min, Max = max };
            var productDto = new ProductDto { Quantity = quantity };

            // Act
            var result = _productService.ProductIsValidFoyBuy(product, productDto);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
