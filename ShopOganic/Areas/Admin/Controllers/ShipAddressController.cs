using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopOganicAPI.Models;
using System.Data;
using System.Text;

namespace ShopOganic.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ShipAddressController : Controller
    {
        // GET: ShipAddressController
        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();
            var apiUrl = "https://localhost:7186/api/ShipAddress/get-all-shipaddress";
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<ShipAddress>>(responseBody);
                return View(list);
            }
            return View();
        }

        // GET: ShipAddressController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShipAddressController/Create
        public  IActionResult Create()
        {
            return View();
        }

        // POST: ShipAddressController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ShipAddress shipAddress)
        {
            try
            {
                var client = new HttpClient();

                var apiUrl = $"https://localhost:7186/api/ShipAddress/create-shipaddress";
                var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
                request.Content = new StringContent(JsonConvert.SerializeObject(shipAddress), Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }

            return View(shipAddress);
        }

        // GET: ShipAddressController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var client = new HttpClient();
            var apiUrl = $"https://localhost:7186/api/ShipAddress/get-shipaddress-{id}";
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var shipAddress = JsonConvert.DeserializeObject<ShipAddress>(await response.Content.ReadAsStringAsync());
                return View(shipAddress);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: ShipAddressController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ShipAddress shipAddress)
        {
            try
            {
                var client = new HttpClient();
                var jsonContent = new StringContent(JsonConvert.SerializeObject(shipAddress), Encoding.UTF8, "application/json");
                var apiUrl = $"https://localhost:7186/api/ShipAddress/update-shipaddress";
                var response = await client.PostAsync(apiUrl, jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: ShipAddressController/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var requestURL = $"https://localhost:7186/api/ShipAddress/delete-shipaddress-{id}";
            var httpClient = new HttpClient();
            var response = await httpClient.DeleteAsync(requestURL);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return BadRequest();
        }

     
    }
}
