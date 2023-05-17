using Moq;
using Sofka.ProductInventory.Aplication;
using Sofka.ProductInventory.Core.Domain.Interfaces;
using Sofka.ProductInventory.Core.Dto;
using Sofka.ProductInventory.Core.Entities;

namespace Sofka.ProductInventory.AplicationTest
{
    public class BuyServicesTest
    {
        private readonly Mock<IBaseRepository<Buy>> _buyRepositoryMock;
        private readonly Mock<IProductServices> _productServicesMock;
        private readonly Mock<IclientService> _clientServiceMock;
        private readonly Mock<IBaseRepository<ProductBuy>> _productBuyRepositoryMock;

        private readonly BuyServices _buyServices;

        public BuyServicesTest()
        {
            _buyRepositoryMock = new Mock<IBaseRepository<Buy>>();
            _productServicesMock = new Mock<IProductServices>();
            _clientServiceMock = new Mock<IclientService>();
            _productBuyRepositoryMock = new Mock<IBaseRepository<ProductBuy>>();

            _buyServices = new BuyServices(_buyRepositoryMock.Object, _productServicesMock.Object, _clientServiceMock.Object, _productBuyRepositoryMock.Object);
        }

        [Fact]
        public async Task  Create_ValidBuyDto_And_CreateBuy_And_UpdateProducts()
        {
            // Arrange
            var buyDto = new BuyDto
            {
                IdClient = 1,
                Date = DateTime.Now,
                Products = new List<ProductDto>
                {
                    new ProductDto { IdProduct = 1, Quantity = 3 },
                    new ProductDto { IdProduct = 2, Quantity = 2 }
                }
            };

            var product1 = new Product { Id = 1, InInventory = 10, Min = 1, Max = 20 };
            var product2 = new Product { Id = 2, InInventory = 5, Min = 1, Max = 10 };

            _clientServiceMock.Setup(x => x.ClientExist(buyDto.IdClient)).Returns(Task.CompletedTask);
            _productServicesMock.Setup(x => x.GetProductById(1)).ReturnsAsync(product1);
            _productServicesMock.Setup(x => x.GetProductById(2)).ReturnsAsync(product2);
            _productServicesMock.Setup(x => x.ProductIsValidFoyBuy(product1, It.IsAny<ProductDto>())).Returns(true);
            _productServicesMock.Setup(x => x.ProductIsValidFoyBuy(product2, It.IsAny<ProductDto>())).Returns(true);

            _buyRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Buy>())).Returns(Task.FromResult(1));
            _productServicesMock.Setup(x => x.Update(It.IsAny<Product>())).Returns(Task.CompletedTask);
            _productBuyRepositoryMock.Setup(x => x.AddAsync(It.IsAny<ProductBuy>())).Returns(Task.FromResult(1));

            // Act
            await _buyServices.Create(buyDto);

            // Assert
            _clientServiceMock.Verify(x => x.ClientExist(buyDto.IdClient), Times.Once);
            _productServicesMock.Verify(x => x.GetProductById(1), Times.Once);
            _productServicesMock.Verify(x => x.GetProductById(2), Times.Once);
            _productServicesMock.Verify(x => x.ProductIsValidFoyBuy(product1, It.IsAny<ProductDto>()), Times.Once);
            _productServicesMock.Verify(x => x.ProductIsValidFoyBuy(product2, It.IsAny<ProductDto>()), Times.Once);
            _buyRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Buy>()), Times.Once);
            _productServicesMock.Verify(x => x.Update(product1), Times.Once);
            _productServicesMock.Verify(x => x.Update(product2), Times.Once);
            _productBuyRepositoryMock.Verify(x => x.AddAsync(It.IsAny<ProductBuy>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Create_InvalidProduct_ThrowsException()
        {
            // Arrange
            var buyDto = new BuyDto
            {
                IdClient = 1,
                Date = DateTime.Now,
                Products = new List<ProductDto>
            {
                new ProductDto { IdProduct = 1, Quantity = 5 }
            }
            };

            var product = new Product { Id = 1, InInventory = 3, Min = 1, Max = 10 };

            _clientServiceMock.Setup(x => x.ClientExist(buyDto.IdClient)).Returns(Task.CompletedTask);
            _productServicesMock.Setup(x => x.GetProductById(1)).ReturnsAsync(product);
            _productServicesMock.Setup(x => x.ProductIsValidFoyBuy(product, It.IsAny<ProductDto>())).Returns(false);

            // Act
            await Assert.ThrowsAsync<Exception>(() => _buyServices.Create(buyDto));

            // Assert
            _clientServiceMock.Verify(x => x.ClientExist(buyDto.IdClient), Times.Once);
            _productServicesMock.Verify(x => x.GetProductById(1), Times.Once);
            _productServicesMock.Verify(x => x.ProductIsValidFoyBuy(product, It.IsAny<ProductDto>()), Times.Once);
            _buyRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Buy>()), Times.Never);
            _productServicesMock.Verify(x => x.Update(It.IsAny<Product>()), Times.Never);
            _productBuyRepositoryMock.Verify(x => x.AddAsync(It.IsAny<ProductBuy>()), Times.Never);
        }

        [Fact]
        public async Task Delete_BuyId_DeletesBuy()
        {
            // Arrange
            var buyId = 1;

            // Act
            await _buyServices.Delete(buyId);

            // Assert
            _buyRepositoryMock.Verify(x => x.DeleteAsync(buyId), Times.Once);
        }

        [Fact]
        public async Task GetBuyById_ValidBuyId_ReturnsBuy()
        {
            // Arrange
            var buyId = 1;
            var expectedBuy = new Buy { Id = buyId };

            _buyRepositoryMock.Setup(x => x.GetByIdAsync(buyId)).ReturnsAsync(expectedBuy);

            // Act
            var result = await _buyServices.GetBuyById(buyId);

            // Assert
            Assert.Equal(expectedBuy, result);
            _buyRepositoryMock.Verify(x => x.GetByIdAsync(buyId), Times.Once);
        }

        [Fact]
        public async Task GetBuys_ReturnsListOfBuys()
        {
            // Arrange
            var expectedBuys = new List<Buy> { new Buy(), new Buy(), new Buy() };

            _buyRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(expectedBuys);

            // Act
            var result = await _buyServices.GetBuys();

            // Assert
            Assert.Equal(expectedBuys, result);
            _buyRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Update_ValidBuy_UpdatesBuy()
        {
            // Arrange
            var buy = new Buy();

            // Act
            await _buyServices.Update(buy);

            // Assert
            _buyRepositoryMock.Verify(x => x.UpdateAsync(buy), Times.Once);
        }
    }
}