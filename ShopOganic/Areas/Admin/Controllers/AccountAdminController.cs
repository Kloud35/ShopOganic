using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ShopOganic.Helpper;
using ShopOganicAPI.Context;
using ShopOganicAPI.Helper;
using ShopOganicAPI.Models;
using System.Data;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;

namespace ShopOganic.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class AccountAdminController : Controller
	{
		private readonly HttpClient _client;
		private OganicDBContext _context;
		private string imageUrl = "";
		public INotyfService _notyfService { get; }
		public AccountAdminController(INotyfService notyfService)
		{
			_client = new HttpClient();
			_context = new OganicDBContext();
			_notyfService = notyfService;
		}

		private void SetViewBagRole(Guid? selectedID = null)
		{
			var x = _context.Roles;

			var lstRole = new List<Role>();
			ViewBag.lstRole = new SelectList(x, "RoleID", "RoleName", selectedID);

		}

		// GET: AccountAdminController
		public async Task<ActionResult> Index()
		{

			List<Account> accountsLst = new List<Account>();
			var apiUrl = "https://localhost:7186/api/Account/get-all-account";
			var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
			// Send request và sử lý response
			var response = await _client.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
				var responseBody = await response.Content.ReadAsStringAsync();
				accountsLst = JsonConvert.DeserializeObject<List<Account>>(responseBody);
				return View(accountsLst);
			}
			return View();
		}

		// GET: AccountAdminController/Details/5
		public async Task<ActionResult> Details(Guid id)
		{
			var role = _context.Roles.FirstOrDefault(c => c.RoleID == id);
			var apiUrl = $"https://localhost:7186/api/Account/get-account-{id}";
			var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
			// Send request và sử lý response

			var response = await _client.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
				var account = JsonConvert.DeserializeObject<Account>(await response.Content.ReadAsStringAsync());
				SetViewBagRole();
				return View(account);
			}

			return RedirectToAction(nameof(Index));
		}

		// GET: AccountAdminController/Create
		public ActionResult Create()
		{

			SetViewBagRole();
			return View();
		}



		// POST: AccountAdminController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(Account account, IFormFile imageFile)
		{
			try
			{
				if (imageFile != null && imageFile.Length > 0)
				{
					//string extension = Path.GetExtension(imageFile.FileName);
					//string imageName = Utilities.SEOUrl(account.FullName) + extension;
					//account.ImageUrl = await Utilities.UploadFile(imageFile, @"accImage", imageName.ToLower());
					account.ImageUrl = await getPic(imageFile);
				}
				if (string.IsNullOrEmpty(account.ImageUrl)) account.ImageUrl = "loi_ap.jpg";
				if (account.LastLogin == null)
				{
					account.LastLogin = DateTime.Now;
				}
				
				var apiUrl = $"https://localhost:7186/api/Account/create-account?";
				account.Password = MD5.EncryptPassword(account.Password);
				var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);//nhận giá trị truyền vào
				request.Content = new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, "application/json");
				var respon = await _client.SendAsync(request);
				if (respon.IsSuccessStatusCode)
				{
					_notyfService.Success("Tạo mới thành công!");
					return RedirectToAction(nameof(Index));
				}
				SetViewBagRole();

				return View();
			}
			catch (Exception ex)
			{
				_notyfService.Error("Lỗi !");
				return BadRequest(ex.Message);
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

		// GET: AccountAdminController/Edit/5
		public async Task<ActionResult> Edit(Guid id)
		{

			var apiUrl = $"https://localhost:7186/api/Account/get-account-{id}";
			var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
			// Send request và sử lý response
			var response = await _client.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
				var account = JsonConvert.DeserializeObject<Account>(await response.Content.ReadAsStringAsync());

				SetViewBagRole();
				return View(account);
			}

			return RedirectToAction(nameof(Index));
		}

		// POST: AccountAdminController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		//[Consumes("multipart/form-data")]
		public async Task<ActionResult> Edit(Account account, IFormFile imageFile)
		{
			try
			{
				if (imageFile != null && imageFile.Length > 0)
				{
					account.ImageUrl = await getPic(imageFile);
				}
				if (string.IsNullOrEmpty(account.ImageUrl)) account.ImageUrl = "loi_ap.jpg";
				var jsonContent = new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, "application/json");
				var apiUrl = $"https://localhost:7186/api/Account/update-account";
				account.Password = MD5.EncryptPassword(account.Password);
				var response = await _client.PostAsync(apiUrl, jsonContent);
				if (response.IsSuccessStatusCode)
				{
					_notyfService.Success("Sửa thành công");
					return RedirectToAction(nameof(Index));
				}
				SetViewBagRole();
				return View();
			}
			catch
			{
				return View();
			}
		}
		/*        [HttpPost]
		*/
		public async Task<ActionResult> Change(Guid id)
		{
			var apiUrl = $"https://localhost:7186/api/Account/get-account-{id}";
			var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
			// Send request và sử lý response
			var response = await _client.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{

				var account = JsonConvert.DeserializeObject<Account>(await response.Content.ReadAsStringAsync());

				if (account.IsActive)
				{
					account.IsActive = false;
				}
				else
				{
					account.IsActive = true;
				}
				var jsonContent = new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, "application/json");
				var apiUrl1 = $"https://localhost:7186/api/Account/update-account";
				var response1 = await _client.PostAsync(apiUrl1, jsonContent);
				if (response1.IsSuccessStatusCode)
				{
					_notyfService.Success("Đổi trạng thái thành công");
					return RedirectToAction("Index");
				}
			}

			return RedirectToAction("Index");
		}
		// GET: AccountAdminController/Delete/5
		public async Task<ActionResult> Delete(Guid id)
		{
			var client = new HttpClient();
			var apiUrl = $"https://localhost:7186/api/Account/delete-account-{id}";
			var response = await client.DeleteAsync(apiUrl);
			_notyfService.Success("Xóa thành công");
			return RedirectToAction(nameof(Index));
		}


	}

}



