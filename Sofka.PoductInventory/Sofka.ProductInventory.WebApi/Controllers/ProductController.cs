using Microsoft.AspNetCore.Mvc;
using Sofka.ProductInventory.Core.Domain.Interfaces;
using Sofka.ProductInventory.Core.Entities;

namespace Sofka.ProductInventory.WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            try
            {
                Product product = await _productServices.GetProductById(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Product> products = await _productServices.GetProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Product product)
        {          
            try
            {
                if (product == null) { return BadRequest(); }
                await _productServices.Create(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] Product product)
        {
            try
            {
                if (product == null) { return BadRequest(); }
                await _productServices.Update(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] int productId)
        {
            try
            {
                await _productServices.Delete(productId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
