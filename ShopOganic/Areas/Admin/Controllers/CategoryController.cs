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
    public class CategoryController : Controller
    {

        // GET: CategoryController
        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();
            var aipUrl = "https://localhost:7186/api/Category/get-all-category";
            var request = new HttpRequestMessage(HttpMethod.Get, aipUrl);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<Category>>(responseBody);
                return View(list);
            }
            return View();
        }

        // GET: CategoryController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var client = new HttpClient();
            var aipUrl = $"https://localhost:7186/api/Category/get-category{id}";
            var request = new HttpRequestMessage(HttpMethod.Get, aipUrl);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var product = JsonConvert.DeserializeObject<Category>(await response.Content.ReadAsStringAsync());
                return View(product);

            }
            return RedirectToAction(nameof(Index));
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Category category)
        {
            try
            {
                var client = new HttpClient();
                var aipUrl = "https://localhost:7186/api/Category/add-category";
                var request = new HttpRequestMessage(HttpMethod.Post, aipUrl);
                request.Content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request);
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

        // GET: CategoryController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var client = new HttpClient();
            var aipUrl = $"https://localhost:7186/api/Category/get-category{id}";
            var request = new HttpRequestMessage(HttpMethod.Get, aipUrl);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var product = JsonConvert.DeserializeObject<Category>(await response.Content.ReadAsStringAsync());
                return View(product);

            }
            return RedirectToAction(nameof(Index));
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Category category)
        {
            try
            {
                var client = new HttpClient();
                var jsonContent = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
                var aipUrl = "https://localhost:7186/api/Category/update-category";
                var request = new HttpRequestMessage(HttpMethod.Post, aipUrl);
                var response = await client.PostAsync(aipUrl, jsonContent);
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

        // GET: CategoryController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var client = new HttpClient();
            var aipUrl = $"https://localhost:7186/api/Category/{id}";
            var request = new HttpRequestMessage(HttpMethod.Delete, aipUrl);
            var response = await client.SendAsync(request);
            return RedirectToAction(nameof(Index));
        }

        
    }
}
