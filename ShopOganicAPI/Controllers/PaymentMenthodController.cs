using Microsoft.AspNetCore.Mvc;
using ShopOganicAPI.Context;
using ShopOganicAPI.IServices;
using ShopOganicAPI.Models;
using ShopOganicAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopOganicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMenthodController : ControllerBase
    {
        private readonly IServices<PaymentMenthod> _ipaymentmenthodservices;
        public PaymentMenthodController()
        {
            Services<PaymentMenthod> _paymentmenthodservices = new Services<PaymentMenthod>();
            _ipaymentmenthodservices = _paymentmenthodservices;
        }
        // GET: api/<PaymentMenthodController>
        [HttpGet("get-all-paymentmenthod")]
        public async Task<IActionResult> GetAllPaymentMenthod()
        {
            var lstpaymentmenthod = await _ipaymentmenthodservices.GetAllAsync();
            if (lstpaymentmenthod == null) return NotFound();
            return Ok(lstpaymentmenthod);
        }

        // GET api/<PaymentMenthodController>/5
        [HttpGet("get-by-id-paymentmenthod/{id}")]
        public async Task<IActionResult> GetByIdPaymentMenthod(Guid id)
        {
            var paymentmenthod = await _ipaymentmenthodservices.GetByIdAsync(id);
            if (paymentmenthod == null) return BadRequest();
            return Ok(paymentmenthod);
        }

        // POST api/<PaymentMenthodController>
        [HttpPost("create-paymentmenthod")]
        public async Task<bool> CreatePaymentMenthod(string paymentmenthodname)
        {
            PaymentMenthod paymentMenthod = new PaymentMenthod()
            {
                PaymentMenthodID = Guid.NewGuid(),
                PaymentMenthodName = paymentmenthodname
            };
            return await _ipaymentmenthodservices.CreateAsync(paymentMenthod);
        }

        // PUT api/<PaymentMenthodController>/5
        [HttpPost("update-paymentmenthod/{id}")]
        public async Task<bool> UpdatePayMentMenthod(Guid id, string paymentmenthodname)
        {
            PaymentMenthod paymentMenthod = new PaymentMenthod()
            {
                PaymentMenthodID = id,
                PaymentMenthodName = paymentmenthodname
            };
            return await _ipaymentmenthodservices.UpdateAsync(paymentMenthod);
        }

        // DELETE api/<PaymentMenthodController>/5
        [HttpDelete("delete-paymentmenthod/{id}")]
        public async Task<bool> DeletePayMentMenthod(Guid id)
        {
            return await _ipaymentmenthodservices.DeleteAsync(id);
        }
    }
}
    