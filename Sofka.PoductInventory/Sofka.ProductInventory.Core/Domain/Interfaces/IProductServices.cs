using Sofka.ProductInventory.Core.Dto;
using Sofka.ProductInventory.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofka.ProductInventory.Core.Domain.Interfaces
{
    public interface IProductServices
    {
        Task<Product> GetProductById(int productId);
        Task<List<Product>> GetProducts();
        Task Create(Product product);
        Task Update(Product product);
        Task Delete(int productId);
        bool ProductIsValidFoyBuy(Product product, ProductDto productDto);
    }
}
