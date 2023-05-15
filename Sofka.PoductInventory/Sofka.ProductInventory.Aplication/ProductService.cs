using Sofka.ProductInventory.Core.Domain.Interfaces;
using Sofka.ProductInventory.Core.Entities;

namespace Sofka.ProductInventory.Aplication
{
    public class ProductService : IProductServices
    {
        private readonly IBaseRepository<Product> _productRepository;

        public ProductService(IBaseRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task CreateProduct(Product product)
        {
            //var products = await _productRepository.GetAllAsync();
            
            Product product1 = new Product { InInventory=1,Name="xiaomi11",Max=5,Min=1};
            await _productRepository.AddAsync(product1);
        }
    }
}
