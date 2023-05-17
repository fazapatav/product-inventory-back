using Microsoft.AspNetCore.Mvc;
using Sofka.ProductInventory.Core.Domain.Interfaces;
using Sofka.ProductInventory.Core.Dto;
using Sofka.ProductInventory.Core.Entities;

namespace Sofka.ProductInventory.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BuyController:ControllerBase
    {
        private readonly IBuyServices _buyServices;

        public BuyController(IBuyServices services)
        {
            _buyServices = services;
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            try
            {
                Buy buy = await _buyServices.GetBuyById(id);
                return Ok(buy);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Buy> buys = await _buyServices.GetBuys();
                return Ok(buys);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] BuyDto buy)
        {
            try
            {
                if (buy == null) { return BadRequest(); }
                await _buyServices.Create(buy);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] Buy buy)
        {
            try
            {
                if (buy == null) { return BadRequest(); }
                await _buyServices.Update(buy);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] int buyId)
        {
            try
            {
                await _buyServices.Delete(buyId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
