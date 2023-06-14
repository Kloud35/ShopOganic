using Microsoft.AspNetCore.Mvc;
using ShopOganicAPI.IServices;
using ShopOganicAPI.Models;
using ShopOganicAPI.Models.DTO;
using ShopOganicAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopOganicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IServices<Cart> _iCartService;
        private readonly IServices<CartDetail> _iCartDetailService;
        private readonly IServices<Product> _iProductService;

        public CartController()
        {
            _iCartService = new Services<Cart>();
            _iCartDetailService = new Services<CartDetail>();
            _iProductService = new Services<Product>();
        }
        // GET: api/<CartController>
        [HttpGet("get-all-cart")]
        public async Task<IActionResult> GetAllCart()
        {
            var lstcart = await _iCartDetailService.GetAllAsync();
            if (lstcart == null) return NotFound();
            return Ok(lstcart);
        }

        // GET api/<CartController>/5
        [HttpGet("get-by-id-cart/{id}")]
        public async Task<IActionResult> GetByIdBill(Guid id)
        {
            var cart = await _iCartDetailService.GetByIdAsync(id);
            if (cart == null) return BadRequest();
            return Ok(cart);
        }

        // POST api/<CartController>
        [HttpPost("add-to-cart")]
        public async Task<bool> AddToCart([FromBody] CartDetailModel model)
        {
            var cart = await _iCartService.GetByIdAsync(model.customerId);
            if (cart == null)
            {
                cart = new Cart()
                {
                    CustomerID = model.customerId
                };
                if (await _iCartService.CreateAsync(cart) == false)
                    return false;
            }

            var product = await _iProductService.GetByIdAsync(model.productId);
            if (product == null)
            {
                return false;
            }
            var cartDetails = await _iCartDetailService.GetAllAsync();
            var cartDetail = cartDetails.FirstOrDefault(p => p.ProductID == model.productId && p.CustomerID == model.customerId);
            if (cartDetail == null)
            {
                cartDetail = new CartDetail()
                {
                    CartDetailID = Guid.NewGuid(),
                    ProductID = model.productId,
                    CustomerID = model.customerId,
                    Quantity = model.quantity
                };
                return await _iCartDetailService.CreateAsync(cartDetail);
            }
            else
            {
                cartDetail.Quantity += model.quantity;
                return await _iCartDetailService.UpdateAsync(cartDetail);
            }
        }

        // PUT api/<CartController>/5
        [HttpPost("update-cart")]
        public async Task<bool> UpdateBill(CartDetail cartDetail)
        {
            return await _iCartDetailService.UpdateAsync(cartDetail);
        }

        // DELETE api/<CartController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
