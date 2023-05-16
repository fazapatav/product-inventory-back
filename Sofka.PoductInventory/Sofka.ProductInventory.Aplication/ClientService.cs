using Sofka.ProductInventory.Core.Domain.Interfaces;
using Sofka.ProductInventory.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofka.ProductInventory.Aplication
{
    public class ClientService : IclientService
    {
        private readonly IBaseRepository<Client> _clientRepository;

        public ClientService(IBaseRepository<Client> clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public async Task Create(Client client)
        {
            await _clientRepository.AddAsync(client);
        }

        public async Task Delete(int clientId)
        {
            await _clientRepository.DeleteAsync(clientId);
        }

        public async Task<Client> GetClientById(int clientId)
        {
            return await _clientRepository.GetByIdAsync(clientId);
        }

        public async Task<List<Client>> GetClients()
        {
            return await _clientRepository.GetAllAsync();
        }

        public async Task Update(Client client)
        {
            await _clientRepository.UpdateAsync(client);
        }
    }
}
