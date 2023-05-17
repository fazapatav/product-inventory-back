using Moq;
using Sofka.ProductInventory.Aplication;
using Sofka.ProductInventory.Core.Domain.Interfaces;
using Sofka.ProductInventory.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofka.ProductInventory.AplicationTest
{
    public class ClientServiceTest
    {
        private readonly Mock<IBaseRepository<Client>> _clientRepositoryMock;
        private readonly ClientService _clientService;

        public ClientServiceTest()
        {
            _clientRepositoryMock = new Mock<IBaseRepository<Client>>();
            _clientService = new ClientService(_clientRepositoryMock.Object);
        }

        [Fact]
        public async Task Create_ValidClient_CreatesClient()
        {
            // Arrange
            var client = new Client();

            // Act
            await _clientService.Create(client);

            // Assert
            _clientRepositoryMock.Verify(x => x.AddAsync(client), Times.Once);
        }

        [Fact]
        public async Task Delete_ClientId_DeletesClient()
        {
            // Arrange
            var clientId = 1;

            // Act
            await _clientService.Delete(clientId);

            // Assert
            _clientRepositoryMock.Verify(x => x.DeleteAsync(clientId), Times.Once);
        }

        [Fact]
        public async Task GetClientById_ValidClientId_ReturnsClient()
        {
            // Arrange
            var clientId = 1;
            var expectedClient = new Client { Id = clientId };

            _clientRepositoryMock.Setup(x => x.GetByIdAsync(clientId)).ReturnsAsync(expectedClient);

            // Act
            var result = await _clientService.GetClientById(clientId);

            // Assert
            Assert.Equal(expectedClient, result);
            _clientRepositoryMock.Verify(x => x.GetByIdAsync(clientId), Times.Once);
        }

        [Fact]
        public async Task GetClients_ReturnsListOfClients()
        {
            // Arrange
            var expectedClients = new List<Client> { new Client(), new Client(), new Client() };

            _clientRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(expectedClients);

            // Act
            var result = await _clientService.GetClients();

            // Assert
            Assert.Equal(expectedClients, result);
            _clientRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Update_ValidClient_UpdatesClient()
        {
            // Arrange
            var client = new Client();

            // Act
            await _clientService.Update(client);

            // Assert
            _clientRepositoryMock.Verify(x => x.UpdateAsync(client), Times.Once);
        }

        [Fact]
        public async Task ClientExist_ClientExists_DoesNotThrowException()
        {
            // Arrange
            var clientId = 1;
            var client = new Client { Id = clientId };

            _clientRepositoryMock.Setup(x => x.GetByIdAsync(clientId)).ReturnsAsync(client);

            // Act & Assert
            await _clientService.ClientExist(clientId);
        }

        [Fact]
        public async Task ClientExist_ClientDoesNotExist_ThrowsException()
        {
            // Arrange
            var clientId = 1;

            _clientRepositoryMock.Setup(x => x.GetByIdAsync(clientId)).ReturnsAsync((Client)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _clientService.ClientExist(clientId));
        }

    }
}


