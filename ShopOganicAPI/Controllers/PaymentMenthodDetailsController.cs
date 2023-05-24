using Microsoft.AspNetCore.Mvc;
using ShopOganicAPI.IServices;
using ShopOganicAPI.Models;
using ShopOganicAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopOganicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMenthodDetailsController : ControllerBase
    {
        private readonly IServices<PaymentMenthodDetail> _ipaymentmenthoddetailservice;

        public PaymentMenthodDetailsController()
        {
            Services<PaymentMenthodDetail> _paymentmenthoddetailservice = new Services<PaymentMenthodDetail>();
            _ipaymentmenthoddetailservice = _paymentmenthoddetailservice;
        }
        // GET: api/<PaymentMenthodDetailsController>
        [HttpGet("get-all-payment-menthod-detail")]
        public async Task<IActionResult> GetAllPaymentMenthodDetail()
        {
            var lstpaymentmenthoddetail = await _ipaymentmenthoddetailservice.GetAllAsync();
            if (lstpaymentmenthoddetail == null) return NotFound();
            return Ok(lstpaymentmenthoddetail);
        }

        // GET api/<PaymentMenthodDetailsController>/5
        [HttpGet("get-by-id-payment-menthod-detail/{id}")]
        public async Task<IActionResult> GetByIdPaymentMenthodDetail(Guid id)
        {
            var paymentmenthoddetail = await _ipaymentmenthoddetailservice.GetByIdAsync(id);
            if (paymentmenthoddetail == null) return BadRequest();
            return Ok(paymentmenthoddetail);
        }

        // POST api/<PaymentMenthodDetailsController>
        [HttpPost("create-payment-menthod-detail")]
        public async Task<bool> CreatePaymentMenthodDetail(Guid paymentmenthodid, Guid billid, string description)
        {
           
            PaymentMenthodDetail paymentMenthodDetail = new PaymentMenthodDetail()
            {
                PaymentMenthodDetailID = Guid.NewGuid(),
                PaymentMenthodID = paymentmenthodid,
                BillID = billid,
                Description = description
            };
            return await _ipaymentmenthoddetailservice.CreateAsync(paymentMenthodDetail);
        }

        // PUT api/<PaymentMenthodDetailsController>/5
        [HttpPost("update-payment-menthod-detail/{id}")]
        public async Task<bool> UpdatePaymentMenthodDetail(Guid id, Guid paymentmenthodid, Guid billid, string description)
        {
           
            PaymentMenthodDetail paymentMenthodDetail = new PaymentMenthodDetail()
            {
                PaymentMenthodDetailID = id,
                PaymentMenthodID = paymentmenthodid,
                BillID = billid,
                Description = description
            };
            return await _ipaymentmenthoddetailservice.UpdateAsync(paymentMenthodDetail);
        }

        // DELETE api/<PaymentMenthodDetailsController>/5
        [HttpDelete("delete-payment-menthod-detail/{id}")]
        public async Task<bool> DeletePaymentMenthodDetail(Guid id)
        {
            return await _ipaymentmenthoddetailservice.DeleteAsync(id);
        }
    }
}
