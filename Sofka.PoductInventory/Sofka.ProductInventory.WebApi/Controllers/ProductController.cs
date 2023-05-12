using Microsoft.AspNetCore.Mvc;
using Sofka.ProductInventory.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Sofka.ProductInventory.Core.Entities;

namespace Sofka.ProductInventory.WebApi.Controllers
{
    /*
    [ApiController]
    [Route("[controller]")]
    public class ProductController: ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        public async Task<IActionResult> Create([FromBody] Product product)
        {
            if (product == null) {return NotFound();}
            _productServices.CreateProduct(product);
            return Ok("todo bn");

        }
    }*/
}
