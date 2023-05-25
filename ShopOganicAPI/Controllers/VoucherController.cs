using Microsoft.AspNetCore.Mvc;
using ShopOganicAPI.IServices;
using ShopOganicAPI.Models;
using ShopOganicAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopOganicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IServices<Voucher> VoucherServices;
        public VoucherController()
        {
            Services<Voucher> services = new Services<Voucher>();
            VoucherServices = services;
        }
        // GET: api/<VoucherController>
        [HttpGet("get-all-voucher")]
        public async Task<IActionResult> GetAllRole()
        {
            var lstVoucher = await VoucherServices.GetAllAsync();
            if (lstVoucher == null)
            {
                return NotFound();
            }
            return Ok(lstVoucher);
        }

        // GET api/<VoucherController>/5
        [HttpGet("{id}")]
        public Task<Voucher> GetVoucherId(Guid id)
        {
            return VoucherServices.GetByIdAsync(id);
        }

        // POST api/<VoucherController>
        [HttpPost("create-voucher")]
        public Task<bool> CreateVoucher(Voucher voucher)
        {
            return VoucherServices.CreateAsync(voucher);
        }

        // PUT api/<VoucherController>/5
        [HttpPut()]
        [Route("update-role")]
        public async Task<bool> UpdateVoucher(Voucher voucher)
        {
            return await VoucherServices.UpdateAsync(voucher);
        }

        // DELETE api/<VoucherController>/5
        [HttpDelete("delete-voucher-{id}")]
        public async Task<bool> DeleteVoucher(Guid id)
        {
            return await VoucherServices.DeleteAsync(id);
        }
    }
}
