using Sofka.ProductInventory.Core.Domain.Interfaces;
using Sofka.ProductInventory.Core.Dto;
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

        public async Task Create(Product product)
        {
            await _productRepository.AddAsync(product);
        }

        public async Task Delete(int productId)
        {
            await _productRepository.DeleteAsync(productId);
        }

        public async Task<Product> GetProductById(int productId)
        {
             return await _productRepository.GetByIdAsync(productId);
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task Update(Product product)
        {
            await _productRepository.UpdateAsync(product);
        }

        public bool ProductIsValidFoyBuy(Product product,ProductDto productDto)
        {
            bool isValid = false;
            bool inInventory = product.InInventory >= productDto.Quantity;
            bool isBetweenMinandMax = productDto.Quantity>= product.Min && productDto.Quantity<= product.Max;
            if (inInventory && isBetweenMinandMax) { isValid = true; }
            return isValid;
        }
    }
}
