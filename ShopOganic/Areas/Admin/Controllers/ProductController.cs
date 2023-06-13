using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopOganicAPI.Models;
using System.Data;
using System.Security.Principal;
using System.Text;


namespace ShopOganic.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        public INotyfService _notyfService { get; }
        // GET: ProductController
        public ProductController(INotyfService notyfService)
        {
            _notyfService = notyfService;
        }
        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();
            var aipUrl = "https://localhost:7186/api/Product/get-all-product";
            var request = new HttpRequestMessage(HttpMethod.Get, aipUrl);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<Product>>(responseBody);
                return View(list);
            }
            return View();
        }

        // GET: ProductController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var client = new HttpClient();
            var aipUrl = $"https://localhost:7186/api/Product/get-product{id}";
            var request = new HttpRequestMessage(HttpMethod.Get, aipUrl);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var product = JsonConvert.DeserializeObject<Product>(await response.Content.ReadAsStringAsync());
                return View(product);

            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product, IFormFile imageFile)
        {
            try
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    product.ImageUrl = await getPic(imageFile);
                }
                if (string.IsNullOrEmpty(product.ImageUrl)) product.ImageUrl = "loi_ap.jpg";
                var client = new HttpClient();
                var aipUrl = "https://localhost:7186/api/Product/add-product";
                var request = new HttpRequestMessage(HttpMethod.Post, aipUrl);
                request.Content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    _notyfService.Success("Tạo mới thành công!");
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                _notyfService.Error("Lỗi !");
                return View();
            }
        }
        public async Task<string> getPic(IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                //Trỏ tới thư mục wwwroot để tí copy sang
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", imageFile.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    //Thực hiện copy ảnh sang thư mục mới wwwroot
                    await imageFile.CopyToAsync(stream);
                }
            }

            return imageFile.FileName;
        }

        // GET: ProductController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var client = new HttpClient();
            var aipUrl = $"https://localhost:7186/api/Product/get-product{id}";
            var request = new HttpRequestMessage(HttpMethod.Get, aipUrl);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var product = JsonConvert.DeserializeObject<Product>(await response.Content.ReadAsStringAsync());
                return View(product);

            }
            return RedirectToAction(nameof(Index));
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Product product, IFormFile imageFile)
        {
            try
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    product.ImageUrl = await getPic(imageFile);
                }
                if (string.IsNullOrEmpty(product.ImageUrl)) product.ImageUrl = "loi_ap.jpg";
                var client = new HttpClient();
                var jsonContent = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                var aipUrl = "https://localhost:7186/api/Product/update-product";
                var request = new HttpRequestMessage(HttpMethod.Post, aipUrl);
                var response = await client.PostAsync(aipUrl, jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    _notyfService.Success("Sửa thành công");
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var client = new HttpClient();
            var aipUrl = $"https://localhost:7186/api/Product/{id}";
            var request = new HttpRequestMessage(HttpMethod.Delete, aipUrl);
            var response = await client.SendAsync(request);
            return RedirectToAction(nameof(Index));
        }
    }
}
