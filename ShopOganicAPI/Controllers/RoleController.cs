using Microsoft.AspNetCore.Mvc;
using ShopOganicAPI.Context;
using ShopOganicAPI.IServices;
using ShopOganicAPI.Models;
using ShopOganicAPI.Services;
using System.Data;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopOganicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IServices<Role> roleServices;

        public RoleController()
        {
            roleServices = new Services<Role>();
        }

        [HttpGet("get-all-role")]
        public async Task<IActionResult> GetAllRole()
        {
            var lstRole = await roleServices.GetAllAsync();
            if(lstRole == null)
            {
                return NotFound();
            }
            return Ok(lstRole);
        }

        [HttpGet("get-role-{id}")]
        public async Task<IActionResult> GetRole(Guid id)
        {
            var role = await roleServices.GetByIdAsync(id);
            if(role == null)
            {
                return BadRequest();
            }
            return Ok(role);
        }

        [HttpPost("create-role")]
        public async Task<bool> CreateRole(Role role)
        {
            return await roleServices.CreateAsync(role);
        }
        [HttpPost("update-role")]
        public async Task<bool> UpdateRole(Role role)
        {
            return await roleServices.UpdateAsync(role);
        }
        [HttpDelete("delete-role-{id}")]
        public async Task<bool> DeleteRole(Guid id)
        {
            return await roleServices.DeleteAsync(id);
        }
    }
}
