using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Sofka.ProductInventory.Core.Domain.Interfaces;
using Sofka.ProductInventory.Core.Dto;
using Sofka.ProductInventory.Core.Entities;

namespace Sofka.ProductInventory.Aplication
{
    public class BuyServices : IBuyServices
    {
        private readonly IBaseRepository<Buy> _buyRepository;
        private readonly IProductServices _productServices;
        private readonly IclientService _clientService;
        private readonly IBaseRepository<ProductBuy> _productBuyRepository;

        public BuyServices(IBaseRepository<Buy> buyRepository, IProductServices productServices, IclientService clientService,IBaseRepository<ProductBuy> productBuyRepository)
        {
            _buyRepository = buyRepository;
            _productServices = productServices;
            _clientService = clientService;
            _productBuyRepository = productBuyRepository;
        }

        public async Task Create(BuyDto buyDto)
        {
            Client client = await _clientService.GetClientById(buyDto.IdClient);
            if (client == null)
            {
                throw new Exception($"El cliente {buyDto.IdClient} no existe");
            }
            List<ProductBuy> productsBuy = new List<ProductBuy>();
            List<Product> products = new List<Product>();
            foreach(ProductDto productDto in buyDto.Products) 
            {
                Product product = await _productServices.GetProductById(productDto.IdProduct);
                if(product == null)
                {
                    throw new Exception($"El producto {productDto.IdProduct} no existe");
                }
                
                if (! _productServices.ProductIsValidFoyBuy(product, productDto))
                {
                    throw new Exception($"Se está comprando el producto {productDto.IdProduct} con una cantidad no permitida ({productDto.Quantity}), disponobilidad:{product.InInventory}, Compra mínima:{product.Min}, compra máxima:{product.Max}");
                }
                ProductBuy productBuy = new ProductBuy {ProductId=product.Id,Quantity=productDto.Quantity};
                productsBuy.Add(productBuy);

                product.InInventory = product.InInventory - productDto.Quantity;
                products.Add(product);
            };

            Buy buy = new Buy { ClientId = buyDto.IdClient,Date = buyDto.Date};
            productsBuy = productsBuy.Select(x => { x.BuyId = 1; return x; }).ToList();

            await _buyRepository.AddAsync(buy);
            foreach(Product product in products)
            {
                await _productServices.Update(product);
            }
            foreach(ProductBuy productBuy in productsBuy)
            {
                await _productBuyRepository.AddAsync(productBuy);
            }

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
