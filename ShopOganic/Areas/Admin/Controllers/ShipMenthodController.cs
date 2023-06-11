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
    public class ShipMenthodController : Controller
    {
        // GET: ShipMenthodController
        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();
            var apiUrl = "https://localhost:7186/api/ShipMenthod/get-all-shipmenthod";
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<ShipMenthod>>(responseBody);
                return View(list);
            }
            return View();
        }

        // GET: ShipMenthodController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShipMenthodController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShipMenthodController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ShipMenthod shipMenthod)
        {
            try
            {
                var client = new HttpClient();

                var apiUrl = $"https://localhost:7186/api/ShipMenthod/create-shipmenthod";
                var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
                request.Content = new StringContent(JsonConvert.SerializeObject(shipMenthod), Encoding.UTF8, "application/json");
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

            return View(shipMenthod);
        }

        // GET: ShipMenthodController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var client = new HttpClient();
            var apiUrl = $"https://localhost:7186/api/ShipMenthod/get-shipmenthod-{id}";
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var shipMenthod = JsonConvert.DeserializeObject<ShipMenthod>(await response.Content.ReadAsStringAsync());
                return View(shipMenthod);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: ShipMenthodController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ShipMenthod shipMenthod)
        {
            try
            {
                var client = new HttpClient();
                var jsonContent = new StringContent(JsonConvert.SerializeObject(shipMenthod), Encoding.UTF8, "application/json");
                var apiUrl = $"https://localhost:7186/api/ShipMenthod/update-shipmenthod";
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

        // GET: ShipMenthodController/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var requestURL = $"https://localhost:7186/api/ShipMenthod/delete-shipmenthod-{id}";
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
