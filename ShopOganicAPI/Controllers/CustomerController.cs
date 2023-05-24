using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopOganicAPI.IServices;
using ShopOganicAPI.Models;
using ShopOganicAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopOganicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        IServices<Customer> customerServices = new Services<Customer>();
        private readonly PasswordHasher<Customer> _passwordHasher = new PasswordHasher<Customer>();
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] Customer customer)
        {
            string hashedPassword = _passwordHasher.HashPassword(customer, customer.Password);
            customer.Password = hashedPassword;
            var result = await customerServices.CreateAsync(customer);

            if (result)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] Customer customer)
        {
            var cus = await customerServices.FindByAttributeAsync(p => p.Email == customer.Email);
            PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(customer, cus.Password,customer.Password);

            if (result == PasswordVerificationResult.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("change-information")]
        public async Task<IActionResult> ChangeInformation([FromBody] Customer customer)
        {
            var result = await customerServices.UpdateAsync(customer);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest();
        }
       
    }
}
