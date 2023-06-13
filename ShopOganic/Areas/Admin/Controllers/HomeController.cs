using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopOganic.Areas.Admin.ViewModels;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using System.Security.Claims;
using ShopOganicAPI.Helper;
using System.Threading.Tasks;
using ShopOganicAPI.Models;
using Newtonsoft.Json;

namespace ShopOganic.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
       
        public HomeController()
        {
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            string password = string.IsNullOrEmpty(login.Password) ? "" : MD5.EncryptPassword(login.Password);
            HttpClient client = new HttpClient();
            var api = "https://localhost:7186/api/Account/get-all-account";
            var response = await client.GetAsync(api);
            List<Account> accounts = new List<Account>();
            if (response.IsSuccessStatusCode)
            {
                accounts = JsonConvert.DeserializeObject<List<Account>>(await response.Content.ReadAsStringAsync());
            }
            var user = accounts.FirstOrDefault(c => c.Email == login.Email && c.Password == password && c.IsActive == true);
            if (user != null)
            {
                var claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Name, user.FullName),
                            new Claim("Name", user.FullName),
                            new Claim("AccountID", user.AccountID.ToString())
                        };
                if (user.RoleID == Guid.Parse("f6ad3e16-8fe4-4cbc-b753-4a941e38d91a"))
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Employee"));
                }
                var identity = new ClaimsIdentity(claims, "MyCookie");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                var auth = new AuthenticationProperties()
                {
                    IsPersistent = true
                };
                await HttpContext.SignInAsync("MyCookie", principal, auth);

                return RedirectToRoute("areas", new { area = "Admin", controller = "Bill", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("", "Đăng nhập thất bại vui lòng kiểm tra lại Email hoặc mật khẩu");
            }
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookie");
            return RedirectToRoute("areas", new { area = "Admin", controller = "Home", action = "Login" });
        }

    }
}
