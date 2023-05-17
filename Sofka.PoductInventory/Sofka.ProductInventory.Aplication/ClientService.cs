using Sofka.ProductInventory.Core.Domain.Interfaces;
using Sofka.ProductInventory.Core.Entities;


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
        public async Task ClientExist(int idclient)
        {
            Client client = await _clientRepository.GetByIdAsync(idclient);
            if (client == null)
            {
                throw new Exception($"El cliente {idclient} no existe");
            }
        }
    }
}
