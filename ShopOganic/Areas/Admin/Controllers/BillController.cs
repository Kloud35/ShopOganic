using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopOganic.Areas.Admin.ViewModels;
using ShopOganicAPI.Models;

namespace ShopOganic.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BillController : Controller
    {
        public INotyfService _notyfService { get; }
        public BillController(INotyfService notyfService)
        {
            _notyfService = notyfService;
        }

        // GET: BillController
        public async Task<ActionResult> Index()
        {
            HttpClient client = new HttpClient();
            var api = "https://localhost:7186/api/Bill/get-all-bill";
            var response = await client.GetAsync(api);
            if (response.IsSuccessStatusCode)
            {
                var objStatus = new List<string>()
            {
                "Đang chuẩn bị","Đang vận chuyển","Giao thành công","Giao thất bại","Đã hủy"
            };
                ViewBag.Status = objStatus;
                var list = JsonConvert.DeserializeObject<List<Bill>>(await response.Content.ReadAsStringAsync());
                return View(list);
            }

            return View();
        }
        public async Task<IActionResult> ChangeStatus([FromBody]BillStatusViewModel billStatus)
        {
            HttpClient client = new HttpClient();
            var api = $"https://localhost:7186/api/Bill/get-by-id-bill/{billStatus.BillID}";
            var response = await client.GetAsync(api);
            if (!response.IsSuccessStatusCode)
            {
                _notyfService.Error("Thay đổi thất bại");
                return BadRequest();
            }
            var bill = JsonConvert.DeserializeObject<Bill>(await response.Content.ReadAsStringAsync());

            bill.Status = billStatus.Status;

            var apiUpdate = "https://localhost:7186/api/Bill/update-bill";
            response = await client.PostAsync(apiUpdate, new StringContent(JsonConvert.SerializeObject(bill), System.Text.Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                _notyfService.Error("Thay đổi thất bại");
                return BadRequest();
            }
            _notyfService.Success("Thay đổi thành công");
            return Ok();
        }

        // GET: BillController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            HttpClient client = new HttpClient();
            var api = $"https://localhost:7186/api/Bill/get-billdetail/{id}";
            var response = await client.GetAsync(api);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            var listDetai = JsonConvert.DeserializeObject<List<BillDetail>>(await response.Content.ReadAsStringAsync());
            var apiBill = $"https://localhost:7186/api/Bill/get-by-id-bill/{id}";
            response = await client.GetAsync(apiBill);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            var bill = JsonConvert.DeserializeObject<Bill>(await response.Content.ReadAsStringAsync());
            BillDetailViewModel billDetailView = new BillDetailViewModel();
            billDetailView.Bill = bill;
            billDetailView.BillDetails = listDetai;
            return View(billDetailView);
        }


    }
}
