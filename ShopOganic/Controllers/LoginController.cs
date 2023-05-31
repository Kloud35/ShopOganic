using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopOganicAPI.Models;
using System.Text;

namespace ShopOganic.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SignUp()
        {
            var httpClient = new HttpClient();
            var apiUrl = "https://localhost:7186/api/Customer/sign-up";
            var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);

            //Tạo customer và convert --> JSON
            var newCustomer = new Customer { /* populate with customer data */ };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(newCustomer), Encoding.UTF8, "application/json");

            // gán jsonContent vào request
            request.Content = jsonContent;

            // Send request và sử lý response
            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<Role>>(responseBody);
                return View(list);
            }
            else
            {
                return View();
            }


        }

        public IActionResult GetView()
        {
            return RedirectToAction("Index","LoginAdmin",new {area = "Admin"});
        }
        public async Task<IActionResult> SignIn()
        {
            var httpClient = new HttpClient();
            var apiUrl = "https://localhost:7186/api/Customer/sign-in";
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);

            var newCustomer = new Customer { Email = "khanh@gmail.com", Password = "1" };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(newCustomer), Encoding.UTF8, "application/json");

            // gán jsonContent vào request
            request.Content = jsonContent;

            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
