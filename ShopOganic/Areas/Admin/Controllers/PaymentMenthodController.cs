using System.Data;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopOganicAPI.Models;

namespace ShopOganic.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PaymentMenthodController : Controller
    {
        [HttpGet]   
        public async Task<IActionResult> PaymentMenthod()
        {
            string requestURL = $"https://localhost:7186/api/PaymentMenthod/get-all-paymentmenthod";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(requestURL);
            string apiData = await response.Content.ReadAsStringAsync();
            var lstobj = JsonConvert.DeserializeObject<List<PaymentMenthod>>(apiData);
            return View(lstobj);
        }   
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            string requestURL = $"https://localhost:7186/api/PaymentMenthod/get-by-id-paymentmenthod/{id}";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(requestURL);
            string apiData = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<PaymentMenthod>(apiData);
            return View(obj);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PaymentMenthod pm)
        {   
            try
            {
                string requestURL = $"https://localhost:7186/api/PaymentMenthod/create-paymentmenthod";
                var httpClient = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(pm), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(requestURL, content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("PaymentMenthod");
                return View();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            try
            {
                string requestURL =
                    $"https://localhost:7186/api/PaymentMenthod/get-by-id-paymentmenthod/{id}";
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(requestURL);
                string apiData = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<PaymentMenthod>(apiData);
                return View(obj);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpPost]  
        public async Task<IActionResult> Update(PaymentMenthod pm)
        {
            try
            {
                var requestURL = $"https://localhost:7186/api/PaymentMenthod/update-paymentmenthod";
                var httpClient = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(pm), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(requestURL, content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("PaymentMenthod");
                }

                return View();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        public async Task<IActionResult> Delete(Guid id)
        {   
            try
            {
                var requestURL = $"https://localhost:7186/api/PaymentMenthod/delete-paymentmenthod/{id}";
                var httpClient = new HttpClient();
                var response = await httpClient.DeleteAsync(requestURL);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("PaymentMenthod");
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(Guid Id)
        {
            var requestURL = $"https://localhost:7186/api/PaymentMenthod/update-paymentmenthod";
            var httpClient = new HttpClient();
            //Lấy obj-by-id
            string requestURLobj = $"https://localhost:7186/api/PaymentMenthod/get-by-id-paymentmenthod/{Id}";
            var httpClientobj = new HttpClient();
            var responseobj = await httpClient.GetAsync(requestURLobj);
            string apiData = await responseobj.Content.ReadAsStringAsync();
            var pm = JsonConvert.DeserializeObject<PaymentMenthod>(apiData);
            //Change status
            if (pm.Status == 1) pm.Status = 0;
            else pm.Status = 1;
            var content = new StringContent(JsonConvert.SerializeObject(pm), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(requestURL, content);
            if(response.IsSuccessStatusCode)
                return Json(new { success = true, status = pm.Status });
            return BadRequest();
        }
    }
}
