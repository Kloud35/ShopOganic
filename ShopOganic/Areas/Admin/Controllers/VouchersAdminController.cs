using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ShopOganicAPI.Context;
using ShopOganicAPI.Models;
using System.Data;
using System.Net;
using System.Text;

namespace ShopOganic.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class VouchersAdminController : Controller
    {
        public INotyfService _notyfService { get; }
/*        private readonly OganicDBContext _dbContext;
*/
        public VouchersAdminController( INotyfService notyfService)
        {
            _notyfService = notyfService;
        }
        // GET: VouchersController
        public async Task<ActionResult> Index()
        {
           

            var client = new HttpClient();
            var apiUrl = "https://localhost:7186/api/Voucher/get-all-voucher";
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            // Send request và sử lý response
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<Voucher>>(responseBody);
                return View(list);
            }
            return View();
        }

        // GET: VouchersController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            List<SelectListItem> lsTrangThai = new List<SelectListItem>();
            lsTrangThai.Add(new SelectListItem() { Text = "Đang sử dụng", Value = "1" });
            lsTrangThai.Add(new SelectListItem() { Text = "Hết hạn", Value = "0" });
            ViewData["lsTrangThai"] = lsTrangThai;

            var client = new HttpClient();
            var apiUrl = $"https://localhost:7186/api/Voucher/{id}";
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            // Send request và sử lý response
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var voucher = JsonConvert.DeserializeObject<Voucher>(await response.Content.ReadAsStringAsync());
                return View(voucher);
            }
            return NotFound();
        }

        // GET: VouchersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VouchersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Voucher vourcher)
        {
            try
            {
                List<SelectListItem> lsTrangThai = new List<SelectListItem>();
                lsTrangThai.Add(new SelectListItem() { Text = "Đang sử dụng", Value = "1" });
                lsTrangThai.Add(new SelectListItem() { Text = "Hết hạn", Value = "0" });
                ViewData["lsTrangThai"] = lsTrangThai;


                if (ModelState.IsValid)
                {
                    var client = new HttpClient();
                    var apiUrl = $"https://localhost:7186/api/Voucher/create-voucher?";

                    var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);// nhận giá trị truyền vào
                    request.Content = new StringContent(JsonConvert.SerializeObject(vourcher), Encoding.UTF8, "application/json");
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        _notyfService.Success("Tạo mới thành công!");
                        return RedirectToAction(nameof(Index));
                    }
                }
             
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: VouchersController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var client = new HttpClient();
            var apiUrl = $"https://localhost:7186/api/Voucher/{id}";
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            // Send request và sử lý response
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var voucher = JsonConvert.DeserializeObject<Voucher>(await response.Content.ReadAsStringAsync());
                return View(voucher);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: VouchersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Voucher voucher)
        {
            try
            {
                List<SelectListItem> lsTrangThai = new List<SelectListItem>();
                lsTrangThai.Add(new SelectListItem() { Text = "Đang sử dụng", Value = "1" });
                lsTrangThai.Add(new SelectListItem() { Text = "Hết hạn", Value = "0" });
                ViewData["lsTrangThai"] = lsTrangThai;

                var client = new HttpClient();
                var jsonContent = new StringContent(JsonConvert.SerializeObject(voucher), Encoding.UTF8, "application/json");
                var apiUrl = $"https://localhost:7186/api/Voucher/update-voucher";
                var response = await client.PostAsync(apiUrl, jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    _notyfService.Success("Chỉnh sửa thành công!");
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: VouchersController/Delete/5
       /* public ActionResult Delete(Guid id)
        {
            return View();
        }*/

        // POST: VouchersController/Delete/5
        /*  [HttpPost]
          [ValidateAntiForgeryToken]*/
        public async Task<ActionResult> Delete(Guid id)
        {
            var client = new HttpClient();
            var apiUrl = $"https://localhost:7186/api/Voucher/delete-voucher-{id}";
            var response = await client.DeleteAsync(apiUrl);
            _notyfService.Success("Xóa thành công!");
            return RedirectToAction(nameof(Index));
        }
    }
}
