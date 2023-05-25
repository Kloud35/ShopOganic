using Microsoft.AspNetCore.Mvc;
using ShopOganicAPI.IServices;
using ShopOganicAPI.Models;
using ShopOganicAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopOganicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IServices<Product> iproductservices;
        public ProductController()
        {
            iproductservices = new Services<Product>();
        }
        // GET: api/<ProductController>
        [HttpGet("get-all-product")]
        public async Task<IActionResult> GetAllProduct()
        {
            var lst = await iproductservices.GetAllAsync();
            if (lst == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(lst);
            }
        }

        // GET api/<ProductController>/5
        [HttpGet("get-product{id}")]
        public async Task<IActionResult> GetProductByID(Guid id)
        {
            var product = await iproductservices.GetByIdAsync(id);
            if (product == null) { return BadRequest(); }
            else { return Ok(product); }
        }

        // POST api/<ProductController>
        [HttpPost("add-product")]
        public async Task<bool> CreateProduct(Product product)
        {
            return await iproductservices.CreateAsync(product);
        }
        [HttpPost("update-product")]
        public async Task<bool> UpdateProduct(Product product)
        {
            return await iproductservices.UpdateAsync(product);
        }


        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<bool> DeleteProduct(Guid id)
        {
            return await iproductservices.DeleteAsync(id);
        }
    }
}
