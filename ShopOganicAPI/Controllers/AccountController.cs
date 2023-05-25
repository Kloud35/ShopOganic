using Microsoft.AspNetCore.Mvc;
using ShopOganicAPI.IServices;
using ShopOganicAPI.Models;
using ShopOganicAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopOganicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IServices<Account> accountServices;

        public AccountController()
        {
            accountServices = new Services<Account>();
        }
        // GET: api/<AccountController>
        [HttpGet("get-all-account")]
        public async Task<IActionResult> GetAllAccount()
        {
            var lstAccount = await accountServices.GetAllAsync();
            if (lstAccount == null)
            {
                return NotFound();
            }
            return Ok(lstAccount);
        }

        // GET api/<AccountController>/5
        [HttpGet("get-account-{id}")]
        public async Task<IActionResult> GetAccountByID(Guid id)
        {
            var account = await accountServices.GetByIdAsync(id);
            if (account == null)
            {
                return BadRequest();
            }
            return Ok(account);
        }
        [HttpPost("create-account")]
        public async Task<bool> CreateAccount(Account account)
        {
            
            return await accountServices.CreateAsync(account);
        }
        [HttpPost("update-account")]
        public async Task<bool> UpdateAccount(Account account)
        {
            return await accountServices.UpdateAsync(account);
        }
        [HttpDelete("delete-account-{id}")]
        public async Task<bool> DeleteAccount(Guid id)
        {
            return await accountServices.DeleteAsync(id);
        }
    }
}
