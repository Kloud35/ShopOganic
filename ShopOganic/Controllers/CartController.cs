
using ShopOganic.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ShopOganic.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using ShopOganicAPI.Models.DTO;
using System.Text;
using ShopOganicAPI.Models;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace ShopOganic.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        public INotyfService _notyfService { get; }
        public CartController(ILogger<CartController> logger,INotyfService notyfService)
        {
            _logger = logger;
            _notyfService = notyfService;
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
        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            GetCustomer();
            if (ViewBag.CustomerID == null)
                return RedirectToAction("Login", "Home");
            var user = ViewBag.CustomerID;
            var client = new HttpClient();
            var aipUrl = $"https://localhost:7186/api/Cart/get-all-cart-{user}";
            var request = new HttpRequestMessage(HttpMethod.Get, aipUrl);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var cartDetails = JsonConvert.DeserializeObject<List<CartDetail>>(responseBody);
                decimal totalAmount = 0;
                var products = new List<Product>();
                foreach (var cartDetail in cartDetails)
                {
                    var aipUrlproduct = $"https://localhost:7186/api/Product/get-product{cartDetail.ProductID}";
                    var requestproduct = new HttpRequestMessage(HttpMethod.Get, aipUrlproduct);
                    var responseproduct = await client.SendAsync(requestproduct);
                    var product = JsonConvert.DeserializeObject<Product>(await responseproduct.Content.ReadAsStringAsync());
                    products.Add(product);
                    totalAmount += (product.Price * cartDetail.Quantity);
                }
                var model = new CartViewModel { CartDetails = cartDetails, Products = products, TotalAmount = totalAmount };
                return View(model);
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid productId, int quantity)
        {
            try
            {
                GetCustomer();
                if (ViewBag.CustomerID == null)
                    return RedirectToAction("Login", "Home");
                var user = ViewBag.CustomerID;
                string requestURL = $"https://localhost:7186/api/Cart/add-to-cart";
                var httpClient = new HttpClient();
                CartDetailModel model = new CartDetailModel()
                {
                    productId = productId,
                    quantity = quantity,
                    customerId = user,
                };
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(requestURL, content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Cart");
                return RedirectToAction("ProductDetail", "Home", new { id = productId });
            }
            catch
            {
                return RedirectToAction("ProductDetail", "Home", new { id = productId });
            }
        }

        public IActionResult UpdateQuantity(CartViewModel model)
        {
            //foreach (var cartDetail in model.CartDetails)
            //{
            //    var cd = _cartDetailService.GetCartDetailById(cartDetail.Id);
            //    cd.Quantity = cartDetail.Quantity;
            //    if (_cartDetailService.UpdateCartDetail(cd) == false)
            //        return HttpNotFound();
            //}
            return RedirectToAction("Cart");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var client = new HttpClient();
            var aipUrl = $"https://localhost:7186/api/Cart/{id}";
            var request = new HttpRequestMessage(HttpMethod.Delete, aipUrl);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                
                return RedirectToAction(nameof(Cart));
            }
            _notyfService.Error("Xóa thất bại");
            return RedirectToAction(nameof(Cart));
        }
        public IActionResult ShowDelete()
        {
            //var cartDetails = SessionService.GetObjFromSession(HttpContext.Session, "Delete");
            //float totalAmount = 0;
            //foreach (var cartDetail in cartDetails)
            //{
            //    totalAmount += cartDetail.Product.Price * cartDetail.Quantity;
            //}
            //var cartViewModel = new CartViewModel { CartDetails = cartDetails, TotalAmount = totalAmount };
            return View(/*"ShowDelete" ,cartViewModel*/);
        }
        [HttpPost]
        public IActionResult RollBack(Guid id)
        {
            //var cartDetails = SessionService.GetObjFromSession(HttpContext.Session, "Delete");
            //var cartDetail = cartDetails.FirstOrDefault(p => p.Id == id);
            //var cartDetailProduct = _cartDetailService.GetAllCartDetail().FirstOrDefault(p => p.ProductId == cartDetail.ProductId);
            //if (cartDetailProduct == null)
            //{
            //    cartDetailProduct = new CartDetail
            //    {
            //        ProductId = cartDetail.ProductId,
            //        CartId = cartDetail.CartId,
            //        Quantity = cartDetail.Quantity,
            //    };
            //    if (_cartDetailService.CreateCartDetail(cartDetailProduct))
            //    {
            //        var check = cartDetails.FirstOrDefault(p => p.ProductId == cartDetail.ProductId);
            //        cartDetails.Remove(check);
            //        SessionService.SetObjToJson(HttpContext.Session, "Delete", cartDetails);
            //        return RedirectToAction("Cart");
            //    }
            //}
            //else
            //{
            //    cartDetailProduct.Quantity += cartDetail.Quantity;
            //    if (_cartDetailService.UpdateCartDetail(cartDetailProduct))
            //    {
            //        var check = cartDetails.FirstOrDefault(p => p.ProductId == cartDetail.ProductId);
            //        cartDetails.Remove(check);
            //        SessionService.SetObjToJson(HttpContext.Session, "Delete", cartDetails);
            //        return RedirectToAction("Cart");
            //    }
            //}
            return HttpNotFound();
        }



        private ActionResult HttpNotFound()
        {
            throw new ArgumentException($"Product with ID not found.");
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
