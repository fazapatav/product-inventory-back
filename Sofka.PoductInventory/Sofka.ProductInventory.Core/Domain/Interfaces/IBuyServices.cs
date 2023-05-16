using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofka.ProductInventory.Core.Dto;
using Sofka.ProductInventory.Core.Entities;

namespace Sofka.ProductInventory.Core.Domain.Interfaces
{
    public interface IBuyServices
    {
        Task<Buy> GetBuyById(int buyId);
        Task<List<Buy>> GetBuys();
        Task Create(BuyDto buy);
        Task Update(Buy buy);
        Task Delete(int buyId);
    }
}
