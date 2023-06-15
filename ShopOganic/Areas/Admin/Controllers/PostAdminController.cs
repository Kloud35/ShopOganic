using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.RulesetToEditorconfig;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShopOganic.Helpper;
using ShopOganicAPI.Context;
using ShopOganicAPI.Models;
using System.Data;
using System.Text;

namespace ShopOganic.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PostAdminController : Controller
    {
        public INotyfService _notyfService { get; }
        private OganicDBContext _context;

        /*        private readonly ShopOganicAPI.Context.OganicDBContext _context;
        */
        public PostAdminController(INotyfService notyfService)
        {
            _notyfService = notyfService;
            _context = new OganicDBContext();

        }
        // GET: PostAdminController
        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();
            var apiUrl = "https://localhost:7186/api/Post/get-all-post";
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            // Send request và sử lý response
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<Post>>(responseBody);

                return View(list);
            }
            return View();
        }

        // GET: PostAdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PostAdminController/Create
        
        private void SetViewBagCate(Guid? selectedID = null)
        {
            var x = _context.Categories;

            var lstCate = new List<Category>();
            ViewBag.lstCate = new SelectList(x, "CategoryID", "CategoryName", selectedID);

        }
        public ActionResult Create()
        {

            SetViewBagCate();
            return View();
        }

        // POST: PostAdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Post post, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            try
            {


                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string imageName = Utilities.SEOUrl(post.Title) + extension;
                    post.ImageUrl = await Utilities.UploadFile(fThumb, @"news", imageName.ToLower());
                }
                if (string.IsNullOrEmpty(post.ImageUrl)) post.ImageUrl = "default.jpg";
                post.Author = "Duy đẹp trai";
                if (post.CategoryID == null)
                {
                    post.CategoryID = Guid.Parse("eded3fa4-aa8a-40d2-9d83-08db6ce24453");
                }
                post.AccountID = Guid.Parse("d1014636-fb58-4f99-9035-eace420e25b2");
                post.Alias = Utilities.SEOUrl(post.Title);
                post.CreatedDate = DateTime.Now;

                var client = new HttpClient();
                var apiUrl = $"https://localhost:7186/api/Post/create-post?";

                var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);// nhận giá trị truyền vào
                request.Content = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");
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
                return BadRequest();
            }
        }

        // GET: PostAdminController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var client = new HttpClient();
            var apiUrl = $"https://localhost:7186/api/Post/get-post-{id}";
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            // Send request và sử lý response
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var post = JsonConvert.DeserializeObject<Post>(await response.Content.ReadAsStringAsync());
                SetViewBagCate();
                return View(post);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: PostAdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Post post, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string imageName = Utilities.SEOUrl(post.Title) + extension;
                        post.ImageUrl = await Utilities.UploadFile(fThumb, @"news", imageName.ToLower());
                    }
                    if (string.IsNullOrEmpty(post.ImageUrl)) post.ImageUrl = "default.jpg";
                    post.Author = "Duy đẹp trai";
                    if (post.CategoryID == null)
                    {
                        post.CategoryID = Guid.Parse("eded3fa4-aa8a-40d2-9d83-08db6ce24453");
                    }
                    post.AccountID = Guid.Parse("d1014636-fb58-4f99-9035-eace420e25b2");
                    post.Alias = Utilities.SEOUrl(post.Title);
                    post.CreatedDate = DateTime.Now;
                    var client = new HttpClient();
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");
                    var apiUrl = $"https://localhost:7186/api/Post/update-post";
                    var response = await client.PostAsync(apiUrl, jsonContent);
                    if (response.IsSuccessStatusCode)
                    {
                        _notyfService.Success("Chỉnh sửa thành công!");
                        return RedirectToAction(nameof(Index));
                    }
                }
                SetViewBagCate();
                return View();
            }
            catch
            {
                return View();
            }
        }

        /* // GET: PostAdminController/Delete/5
         public ActionResult Delete(int id)
         {
             return View();
         }*/

        // POST: PostAdminController/Delete/5
        /*[HttpPost]
        [ValidateAntiForgeryToken]*/
        public async Task<ActionResult> Delete(Guid id)
        {
            var client = new HttpClient();
            var apiUrl = $"https://localhost:7186/api/Post/delete-post-{id}";
            var response = await client.DeleteAsync(apiUrl);
            _notyfService.Success("Xóa thành công!");
            return RedirectToAction(nameof(Index));
        }
    }
}
