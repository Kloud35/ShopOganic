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
    public class PostController : ControllerBase
    {
        private readonly IServices<Post> PostServices;
        private OganicDBContext context = new OganicDBContext();
        public PostController()
        {
            Services<Post> services = new Services<Post>();
            PostServices = services;
        }
        // GET: api/<PostController>
        [HttpGet("get-all-post")]
        public async Task<IActionResult> GetAllPost()
        {
            var lstPost = await PostServices.GetAllAsync();
            if (lstPost == null)
            {
                return NotFound();
            }
            return Ok(lstPost);
        }

        // GET api/<PostController>/5
        [HttpGet("get-post-{id}")]
        public Task<Post> GetPostId(Guid id)
        {
            return PostServices.GetByIdAsync(id);
        }

        // POST api/<PostController>
        [HttpPost("create-post")]
        public async Task<IActionResult> Post([FromBody] Post post)
        {
            if (await PostServices.CreateAsync(post))
            {
                return Ok();
            }
            return BadRequest();
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Post post)
        {
            if (await PostServices.UpdateAsync(post))
            {
                return Ok();
            }
            return BadRequest();
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(Guid id)
        {
            return await PostServices.DeleteAsync(id);
        }
    }
}
