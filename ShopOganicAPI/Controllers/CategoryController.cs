using Microsoft.AspNetCore.Mvc;
using ShopOganicAPI.IServices;
using ShopOganicAPI.Models;
using ShopOganicAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopOganicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IServices<Category> icategoryservices;
        public CategoryController()
        {
            icategoryservices = new Services<Category>();
        }
        // GET: api/<CategoryController>
        [HttpGet("get-all-category")]
        public async Task<IActionResult> GetAllCategory()
        {
            var lst = await icategoryservices.GetAllAsync();
            if (lst == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(lst);
            }
        }

        // GET api/<CategoryController>/5
        [HttpGet("get-category{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var category = await icategoryservices.GetByIdAsync(id);
            if (category == null)
            {
                return BadRequest();
            }
            return Ok(category);
        }

        // POST api/<CategoryController>
        [HttpPost("add-category")]
        public async Task<bool> CreateCategory(string CategoryName, int Status, int Published)
        {
            Category category = new Category();
            category.CategoryID = Guid.NewGuid();
            category.CategoryName = CategoryName;
            category.Status = Status;
            category.Published = Published;
            return await icategoryservices.CreateAsync(category);
        }
        [HttpPost("update-category")]
        public async Task<bool> UpdateCategory(Guid ID, string CategoryName, int Status, int Published)
        {
            Category category = new Category();
            category.CategoryID = ID;
            category.CategoryName = CategoryName;
            category.Status = Status;
            category.Published = Published;
            return await icategoryservices.UpdateAsync(category);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<bool> DeleteCategory(Guid id)
        {
            return await icategoryservices.DeleteAsync(id);
        }
    }
}
