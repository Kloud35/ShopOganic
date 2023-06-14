
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ShopOganic.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;
using ShopOganic.Models;
using Newtonsoft.Json;
using ShopOganic.ViewModel;
using ShopOganicAPI.Models;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace ShopOganic.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;

		}
		public void GetCustomer()
		{
			string cookieValue = Request.Cookies["MyCookie"];

			if (!string.IsNullOrEmpty(cookieValue))
			{
				// Get the ClaimsPrincipal from the cookie
				var principal = HttpContext.User;

				if (principal != null && principal.Identity.IsAuthenticated && principal.IsInRole("Customer"))
				{
					ViewBag.CustomerID = Guid.Parse(principal.FindFirst("CustomerID").Value);
					ViewBag.Name = principal.FindFirst("Name").Value;
				}
			}
		}
		public async Task<IActionResult> Index()
		{
			GetCustomer();
			var client = new HttpClient();
			var aipUrl = "https://localhost:7186/api/Product/get-all-product";
			var request = new HttpRequestMessage(HttpMethod.Get, aipUrl);
			var response = await client.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
				var responseBody = await response.Content.ReadAsStringAsync();
				var list = JsonConvert.DeserializeObject<List<Product>>(responseBody).Where(p => p.Quantity > 0);
				return View(list);
			}
			return View();
		}
		public async Task<IActionResult> Shop()
		{
			var client = new HttpClient();
			var aipUrl = "https://localhost:7186/api/Product/get-all-product";
			var request = new HttpRequestMessage(HttpMethod.Get, aipUrl);
			var response = await client.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
				var responseBody = await response.Content.ReadAsStringAsync();
				var list = JsonConvert.DeserializeObject<List<Product>>(responseBody).Where(p => p.Status == 1);
				return View(list);
			}
			return View();
		}
		public IActionResult Search(string search)
		{
			if (string.IsNullOrEmpty(search))
			{
				return RedirectToAction("Shop");
			}
			//var check = _productService.GetProductByName(search);
			//var viewModel = new ProductViewModel() { Products = check };
			return View(/*"Shop", viewModel*/);
		}
		public async Task<IActionResult> ProductDetail(Guid id)
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
			return View();
		}

		public IActionResult Contact()
		{
			//TempData["CartItem"] = _cartDetailService.GetAllCartDetail().Count;
			var username = HttpContext.Session.GetString("UserLoginSession");
			TempData["UserLogin"] = username;
			return View("Contact");
		}
		//Hoàng
		public async Task<IActionResult> Blog()
		{

			var client = new HttpClient();
			var aipUrl = "https://localhost:7186/api/Post/get-all-post";
			var request = new HttpRequestMessage(HttpMethod.Get, aipUrl);
			var response = await client.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
				var responseBody = await response.Content.ReadAsStringAsync();
				var lst = JsonConvert.DeserializeObject<List<Post>>(responseBody);
				return View(lst);
			}
			return View();
		}
		//Hoàng
		public async Task<IActionResult> BlogDetail(Guid id)
		{

			var client = new HttpClient();
			var apiUrl = $"https://localhost:7186/api/Post/get-post-{id}";
			var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
			var response = await client.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
				var post = JsonConvert.DeserializeObject<Post>(await response.Content.ReadAsStringAsync());
				return View(post);
			}

			return RedirectToAction(nameof(Index));
		}
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginCustomerViewModel model)
		{
			var client = new HttpClient();
			var apiUrl = "https://localhost:7186/api/Customer/sign-in";
			var customer = new Customer
			{
				Email = model.Email,
				Password = model.Password,
			};
			var jsonContent = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
			var response = await client.PostAsync(apiUrl, jsonContent);
			if (response.IsSuccessStatusCode)
			{
				var check = JsonConvert.DeserializeObject<Customer>(await response.Content.ReadAsStringAsync());
				var claims = new List<Claim>()
				{
					new Claim(ClaimTypes.Name, check.FullName),
					new Claim("Name", check.FullName),
					new Claim("CustomerID", check.CustomerID.ToString()),
					new Claim("Email", check.Email),
					new Claim(ClaimTypes.Role, "Customer")
				};
				var identity = new ClaimsIdentity(claims, "MyCookie");
				ClaimsPrincipal principal = new ClaimsPrincipal(identity);
				var auth = new AuthenticationProperties()
				{
					IsPersistent = true
				};
				await HttpContext.SignInAsync("MyCookie", principal, auth);
				return RedirectToAction(nameof(Index));
			}

			return View();
		}
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(LoginCustomerViewModel model)
		{
			var customer = new Customer
			{
				FullName = model.FullName,
				Email = model.Email,
				Password = model.Password,
				CreatedDate = DateTime.Now
			};
			var httpClient = new HttpClient();
			var apiUrl = "https://localhost:7186/api/Customer/sign-up";

			var jsonContent = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
			// Send request và sử lý response
			var response = await httpClient.PostAsync(apiUrl, jsonContent);

			if (!response.IsSuccessStatusCode)
			{
				return View();
			}

			return RedirectToAction(nameof(Login));
		}



		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync("MyCookie");
			return RedirectToAction("Index", "Home");
		}
	}
}