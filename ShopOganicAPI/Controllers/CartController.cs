using Microsoft.AspNetCore.Mvc;
using ShopOganicAPI.IServices;
using ShopOganicAPI.Models;
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

        public CartController()
        {
            _iCartService = new Services<Cart>();
            _iCartDetailService = new Services<CartDetail>();
        }

        // GET: api/<CartController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CartController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CartController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CartController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CartController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
