using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopOganicAPI.Models;
using System.Data;
using System.Net.Http.Json;
using System.Text;

namespace ShopOganic.Controllers
{
    public class RoleController : Controller
    {
        // GET: RoleController
        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();
            var apiUrl = "https://localhost:7186/api/Role/get-all-role";
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            // Send request và sử lý response
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<Role>>(responseBody);
                return View(list);
            }
            return View();
        }

        // GET: RoleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RoleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Role role)
        {
            try
            {
                var client = new HttpClient();

                var apiUrl = $"https://localhost:7186/api/Role/create-role?RoleCode={role.RoleCode}&RoleName={role.RoleName}&Description={role.Description}";
                var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
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

        // GET: RoleController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var client = new HttpClient();
            var apiUrl = $"https://localhost:7186/api/Role/get-role-{id}";
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            // Send request và sử lý response
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var role = JsonConvert.DeserializeObject<Role>( await response.Content.ReadAsStringAsync());
                return View(role);
            }
           
            return RedirectToAction(nameof(Index));
        }

        // POST: RoleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Edit(Guid id, Role role)
        {
            try
            {
                var client = new HttpClient();
                var jsonContent = new StringContent(JsonConvert.SerializeObject(role), Encoding.UTF8, "application/json");
                var apiUrl = $"https://localhost:7186/api/Role/update-role";
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

        // GET: RoleController/Delete/5
        public async Task< ActionResult> Delete(Guid id)
        {
            var client = new HttpClient();
            var apiUrl = $"https://localhost:7186/api/Role/delete-role-{id}";
            var response = await client.DeleteAsync(apiUrl);
            return RedirectToAction(nameof(Index));
        }

        
    }
}
