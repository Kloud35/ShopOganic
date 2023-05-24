using Microsoft.AspNetCore.Mvc;
using ShopOganicAPI.IServices;
using ShopOganicAPI.Models;
using ShopOganicAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopOganicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IServices<Bill> _ibillservice;

        public BillController()
        {
            Services<Bill> _billservice = new Services<Bill>();
            _ibillservice = _billservice;
        }
        // GET: api/<BillController>
        [HttpGet("get-all-bill")]
        public async Task<IActionResult> GetAllBill()
        {
            var lstbill = await _ibillservice.GetAllAsync();
            if (lstbill == null) return NotFound();
            return Ok(lstbill);
        }

        // GET api/<BillController>/5
        [HttpGet("get-by-id-bill/{id}")]
        public async Task<IActionResult> GetByIdBill(Guid id)
        {
            var bill = await _ibillservice.GetByIdAsync(id);
            if (bill == null) return BadRequest();
            return Ok(bill);
        }

        // POST api/<BillController>
        [HttpPost("create-bill")]
        public async Task<bool> CreateBill(Guid customerid, Guid voucherid, Guid shipaddressid, Guid shipmenthodid, string billcode, string paymentmenthod, decimal totalmoney, string receivername, string customerphone, string addressdelivery, int status)
        {
          
            Bill bill = new Bill()
            {
                BillID = Guid.NewGuid(),
                CustomerID = customerid,
                VoucherID = voucherid,
                ShipAddressID = shipaddressid,
                ShipMenthodID = shipmenthodid,
                BillCode = billcode,
                PaymentMenthod = paymentmenthod,
                TotalMoney = totalmoney,
                ReceiverName = receivername,
                CustomerPhone = customerphone,
                AddressDelivery = addressdelivery,
                CreatedDate = DateTime.Now,
                Status = status
            };
            return await _ibillservice.CreateAsync(bill);
        }

        // PUT api/<BillController>/5
        [HttpPost("update-bill/{id}")]
        public async Task<bool> UpdateBill(Guid id, Guid customerid, Guid voucherid, Guid shipaddressid, Guid shipmenthodid, string billcode, string paymentmenthod, decimal totalmoney, string receivername, string customerphone, string addressdelivery, int status)
        {
           
            Bill bill = new Bill()
            {
                BillID = id,
                CustomerID = customerid,
                VoucherID = voucherid,
                ShipAddressID = shipaddressid,
                ShipMenthodID = shipmenthodid,
                BillCode = billcode,
                PaymentMenthod = paymentmenthod,
                TotalMoney = totalmoney,
                ReceiverName = receivername,
                CustomerPhone = customerphone,
                AddressDelivery = addressdelivery,
                CreatedDate = DateTime.Now,
                Status = status
            };
            return await _ibillservice.UpdateAsync(bill);
        }

        // DELETE api/<BillController>/5
        [HttpDelete("delete-bill/{id}")]
        public async Task<bool> DeleteBill(Guid id)
        {
            return await _ibillservice.DeleteAsync(id);
        }
    }
}
