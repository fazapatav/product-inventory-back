using Microsoft.AspNetCore.Mvc;
using Sofka.ProductInventory.Aplication;
using Sofka.ProductInventory.Core.Domain.Interfaces;
using Sofka.ProductInventory.Core.Entities;

namespace Sofka.ProductInventory.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController:ControllerBase
    {
        private readonly IclientService _clientService;

        public ClientController(IclientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            try
            {
                Client  client = await _clientService.GetClientById(id);
                return Ok(client);
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
                List<Client> clients = await _clientService.GetClients();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Client client)
        {
            try
            {
                if (client == null) { return BadRequest(); }
                await _clientService.Create(client);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] Client client)
        {
            try
            {
                if (client == null) { return BadRequest(); }
                await _clientService.Update(client);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] int clientId)
        {
            try
            {
                await _clientService.Delete(clientId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
