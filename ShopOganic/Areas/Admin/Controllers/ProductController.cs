using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ShopOganic.Helpper;
using ShopOganicAPI.Context;
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
        private OganicDBContext _context;

        // GET: ProductController
        public ProductController(INotyfService notyfService)
        {
            _notyfService = notyfService;
            _context = new OganicDBContext();
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
        private void SetViewBagCate(Guid? selectedID = null)
        {
            var x = _context.Categories;

            var lstCate = new List<Category>();
            ViewBag.lstCate = new SelectList(x, "CategoryID", "CategoryName", selectedID);

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
                SetViewBagCate();
                return View(product);

            }
            return RedirectToAction(nameof(Index));
        }
     
        // GET: ProductController/Create
        public ActionResult Create()
        {
            SetViewBagCate();
            return View();
        }


        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product, Microsoft.AspNetCore.Http.IFormFile fThumb)
		{
            try
            {
               
                if (product.CreatedDate == null)
                {
                    product.CreatedDate = DateTime.Now;
                }
                if (product.Price == null || product.Quantity == null )
                {
                    product.Price = 100000;
                    product.Quantity = 100;
                }

				if (fThumb != null)
				{
					string extension = Path.GetExtension(fThumb.FileName);
					string imageName = Utilities.SEOUrl(product.ProductName) + extension;
					product.ImageUrl = await Utilities.UploadFile(fThumb, @"news", imageName.ToLower());
				}
				if (string.IsNullOrEmpty(product.ImageUrl)) product.ImageUrl = "default.jpg";
                product.Status = 1;
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
                SetViewBagCate();
                return View();
            }
            catch
            {
                _notyfService.Error("Lỗi !");
                return View();
            }
        }
       /* public async Task<string> getPic(IFormFile imageFile)
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
        }*/

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
				SetViewBagCate();
				return View(product);

            }
            return RedirectToAction(nameof(Index));
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Product product, Microsoft.AspNetCore.Http.IFormFile fThumb)
		{
            try
            {
				if (fThumb != null)
				{
					string extension = Path.GetExtension(fThumb.FileName);
					string imageName = Utilities.SEOUrl(product.ProductName) + extension;
					product.ImageUrl = await Utilities.UploadFile(fThumb, @"news", imageName.ToLower());
				}
				if (string.IsNullOrEmpty(product.ImageUrl)) product.ImageUrl = "default.jpg";
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
				SetViewBagCate();
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
