using Microsoft.AspNetCore.Mvc;
using ShopOganicAPI.IServices;
using ShopOganicAPI.Services;
using ShopOganicAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopOganicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipAddressController : ControllerBase
    {
        private readonly IServices<ShipAddress> shipAddressService;
        public ShipAddressController()
        {
            shipAddressService = new Services<ShipAddress>();
        }
        // GET: api/<ShipAddressController>
        [HttpGet("get-all-shipaddress")]
        public async Task<IActionResult> GetAllShipAddress()
        {
            var lstShipAddress = await shipAddressService.GetAllAsync();
            if (lstShipAddress == null)
            {
                return NotFound();
            }
            return Ok(lstShipAddress);
        }

        // GET api/<ShipAddressController>/5
        [HttpGet("get-shipaddress-{id}")]
        public async Task<IActionResult> Getshipaddress(Guid id)
        {
            var shipAddress = await shipAddressService.GetByIdAsync(id);
            if (shipAddress == null)
            {
                return BadRequest();
            }
            return Ok(shipAddress);
        }

        // POST api/<ShipAddressController>
        [HttpPost("create-shipaddress")]
        public async Task<bool> CreateShipAddress(ShipAddress shipAddress)
        {
            return await shipAddressService.CreateAsync(shipAddress);
        }

        // PUT api/<ShipAddressController>/5
        [HttpPost("update-shipaddress")]
        public async Task<bool> UpdateShipAddress(ShipAddress shipAddress)
        {
            return await shipAddressService.UpdateAsync(shipAddress);
        }

        // DELETE api/<ShipAddressController>/5
        [HttpDelete("delete-shipaddress-{id}")]
        public async Task<bool> DeleteShipAddress(Guid id)
        {
            return await shipAddressService.DeleteAsync(id);
        }
    }
}
