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
    public class RolesAdminController : Controller
    {
        private readonly HttpClient _client;
        public RolesAdminController()
        {
            _client = new HttpClient();
        }
        // GET: RolesAdminController
        public async Task<ActionResult> Index()
        {

            List<Role> rolesLst = new List<Role>();            
            var apiUrl = "https://localhost:7186/api/Role/get-all-role";
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            // Send request và sử lý response
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                rolesLst = JsonConvert.DeserializeObject<List<Role>>(responseBody);
                return View(rolesLst);
            }
            return View();
        }

        // GET: RolesAdminController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var apiUrl = $"https://localhost:7186/api/Role/get-role-{id}";
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            // Send request và sử lý response
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var role = JsonConvert.DeserializeObject<Role>(await response.Content.ReadAsStringAsync());
                return View(role);
            }

            return RedirectToAction(nameof(Index));

        }

        // GET: RolesAdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RolesAdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Role role)
        {
            try
            {
                
                var apiUrl = $"https://localhost:7186/api/Role/create-role?";

                var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);//nhận giá trị truyền vào
                request.Content = new StringContent(JsonConvert.SerializeObject(role), Encoding.UTF8, "application/json");
                var respon = await _client.SendAsync(request);
                if (respon.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Role Created";
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return BadRequest(ex.Message);
            }
        }

        // GET: RolesAdminController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            
            var apiUrl = $"https://localhost:7186/api/Role/get-role-{id}";
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            // Send request và sử lý response
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var role = JsonConvert.DeserializeObject<Role>(await response.Content.ReadAsStringAsync());
                return View(role);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: RolesAdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Role role)
        {
            try
            {
                
                var jsonContent = new StringContent(JsonConvert.SerializeObject(role), Encoding.UTF8, "application/json");
                var apiUrl = $"https://localhost:7186/api/Role/update-role";
                var response = await _client.PostAsync(apiUrl, jsonContent);
                if (response.IsSuccessStatusCode)
                {
					TempData["successMessage"] = "Role Edit";
					return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
		

		// GET: RolesAdminController/Delete/5
		public async Task<ActionResult> Delete(Guid id)
        {
            var client = new HttpClient();
            var apiUrl = $"https://localhost:7186/api/Role/delete-role-{id}";
            var response = await client.DeleteAsync(apiUrl);
			TempData["successMessage"] = "Role Delete";
			return RedirectToAction(nameof(Index));
        }
    }
}
