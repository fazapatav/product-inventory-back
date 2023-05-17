using Sofka.ProductInventory.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofka.ProductInventory.Core.Domain.Interfaces
{
    public interface IclientService
    {
        Task<Client> GetClientById(int clientId);
        Task<List<Client>> GetClients();
        Task Create(Client client);
        Task Update(Client client);
        Task Delete(int clientId);
        Task ClientExist(int clientId);

    }
}
