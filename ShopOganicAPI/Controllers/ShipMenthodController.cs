using Microsoft.AspNetCore.Mvc;
using ShopOganicAPI.IServices;
using ShopOganicAPI.Services;
using ShopOganicAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopOganicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipMenthodController : ControllerBase
    {
        private readonly IServices<ShipMenthod> shipMenthodService;
        public ShipMenthodController()
        {
            shipMenthodService = new Services<ShipMenthod>();
        }
        // GET: api/<ShipMethodController>
        [HttpGet("get-all-shipmenthod")]
        public async Task<IActionResult> GetAllShipMenthod()
        {
            var lstShipMenthod = await shipMenthodService.GetAllAsync();
            if (lstShipMenthod == null)
            {
                return NotFound();
            }
            return Ok(lstShipMenthod);
        }

        // GET api/<ShipMethodController>/5
        [HttpGet("get-shipmenthod-{id}")]
        public async Task<IActionResult> GetShipMenthod(Guid id)
        {
            var shipMenthod = await shipMenthodService.GetByIdAsync(id);
            if (shipMenthod == null)
            {
                return BadRequest();
            }
            return Ok(shipMenthod);
        }

        // POST api/<ShipMethodController>
        [HttpPost("create-shipmenthod")]
        public async Task<bool> CreateShipMenthod(ShipMenthod shipMenthod)
        {
            return await shipMenthodService.CreateAsync(shipMenthod);
        }

        // PUT api/<ShipMethodController>/5
        [HttpPost("update-shipsenthod")]
        public async Task<bool> UpdateShipMenthod(ShipMenthod shipMenthod)
        {
            return await shipMenthodService.UpdateAsync(shipMenthod);
        }

        // DELETE api/<ShipMethodController>/5
        [HttpDelete("delete-shipmenthod{id}")]
        public async Task<bool> DeleteShipMenthod(Guid id)
        {
            return await shipMenthodService.DeleteAsync(id);
        }
    }
}
