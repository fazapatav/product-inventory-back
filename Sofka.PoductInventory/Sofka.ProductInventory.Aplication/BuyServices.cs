using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofka.ProductInventory.Core.Domain.Interfaces;
using Sofka.ProductInventory.Core.Entities;

namespace Sofka.ProductInventory.Aplication
{
    public class BuyServices : IBuyServices
    {
        private readonly IBaseRepository<Buy> _buyRepository;

        public BuyServices(IBaseRepository<Buy> buyRepository)
        {
            _buyRepository = buyRepository;
        }

        public async Task Create(Buy buy)
        {
            await _buyRepository.AddAsync(buy);
        }

        public async Task Delete(int buyId)
        {
            await _buyRepository.DeleteAsync(buyId);
        }

        public async Task<Buy> GetBuyById(int buyId)
        {
            return await _buyRepository.GetByIdAsync(buyId);
        }

        public async Task<List<Buy>> GetBuys()
        {
            return await _buyRepository.GetAllAsync();
        }

        public async Task Update(Buy buy)
        {
            await _buyRepository.UpdateAsync(buy);
        }
    }
}
