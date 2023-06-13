using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopOganicAPI.IServices;
using ShopOganicAPI.Models;
using ShopOganicAPI.Models.DTO;
using ShopOganicAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopOganicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IServices<Bill> _ibillservice;
        private readonly IServices<BillDetail> _ibillDetailservice;
        private readonly IServices<Product> _iProduct;
        private readonly IServices<Voucher> _iVoucher;
        public BillController()
        {
            _ibillservice = new Services<Bill>();
            _ibillDetailservice = new Services<BillDetail>();
            _iProduct = new Services<Product>();
            _iVoucher = new Services<Voucher>();
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
        public async Task<bool> CreateBill(BillModel billModel)
        {
            var bill = await _ibillservice.CreateAsync(billModel.Bill);
            decimal totalBillMoney = 0;
            var voucher = await _iVoucher.GetByIdAsync(billModel.Bill.VoucherID);
            if (bill)
            {
                foreach (var item in billModel.BillDetail)
                {
                    var product = await _iProduct.GetByIdAsync(item.ProductID);
                    item.BillID = billModel.Bill.BillID;
                    var model = await _ibillDetailservice.FindByAttributeAsync(p => p.BillID == billModel.Bill.BillID && p.ProductID == item.ProductID);
                    if (model == null)
                    {
                        item.Price = (decimal)product.Price;
                        item.TotalMoney = item.Price * item.Quantity;
                        await _ibillDetailservice.CreateAsync(item);
                    }
                    else
                    {
                        model.Price = (decimal)product.Price;
                        item.TotalMoney = model.Price * item.Quantity;
                        model.Quantity += item.Quantity;
                        model.TotalMoney = model.Price * model.Quantity;
                        await _ibillDetailservice.UpdateAsync(model);
                    }
                    totalBillMoney += item.TotalMoney;
                    product.Quantity -= item.Quantity;
                    await _iProduct.UpdateAsync(product);
                }
                billModel.Bill.TotalMoney = totalBillMoney - (totalBillMoney * (voucher.PercentDiscount / 100));
                billModel.Bill.Status = 1;
                await _ibillservice.UpdateAsync(billModel.Bill);
                return true;
            }
            return false;
        }

        // PUT api/<BillController>/5
        [HttpPost("update-bill")]
        public async Task<bool> UpdateBill(Bill bill)
        {
            return await _ibillservice.UpdateAsync(bill);
        }
        [HttpGet("get-billdetail/{id}")]
        public async Task<IActionResult> GetBillDetail(Guid id)
        {
            var billDetail = await _ibillDetailservice.SearchAsync(p=>p.BillID == id);
            if (billDetail == null)
            {
                return BadRequest(string.Empty);
            }
            return Ok(billDetail);
        }
    }
}
