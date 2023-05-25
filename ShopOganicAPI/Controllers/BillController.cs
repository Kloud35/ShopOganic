﻿using Microsoft.AspNetCore.Mvc;
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
            _ibillservice = new Services<Bill>(); ;
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
        public async Task<bool> CreateBill(Bill bill)
        {
            return await _ibillservice.CreateAsync(bill);
        }

        // PUT api/<BillController>/5
        [HttpPost("update-bill/{id}")]
        public async Task<bool> UpdateBill(Bill bill)
        {
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
