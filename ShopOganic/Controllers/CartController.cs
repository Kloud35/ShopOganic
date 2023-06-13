
using ShopOganic.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ShopOganic.Models;
using System.Diagnostics;

namespace ShopOganic.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        //private readonly IProductService _productService;
        //private readonly ICartService _cartService; 
        //private readonly ICartDetailService _cartDetailService;

        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
            //_productService = new ProductService();
            //_cartService = new CartService();   
            //_cartDetailService = new CartDetailService();
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
        public async Task<IActionResult> Cart()
        {

            return View();
        }
        [HttpPost]
        public IActionResult AddToCart(Guid productId, int quantity)
        {
            //Kiểm tra xem Giỏ hàng hiện tại của người dùng

            //C1
            //var cart =_cartService.GetCartById(currentUserId);

            //if (cart == null) 
            //{
            //    cart = new Cart { UserId = currentUserId };
            //    _cartService.CreateCart(cart);
            //}

            //C2
            //var cart = _cartService.GetCartById(User.Identity.GetUserId());

            //if (cart == null)
            //{
            //    cart = new Cart { UserId = User.Identity.GetUserId() };
            //}

            //var product = _productService.GetProductById(productId);

            //if (product == null)
            //{
            //    return HttpNotFound();
            //}

            //var cartDetail = _cartDetailService.GetAllCartDetail().FirstOrDefault(p => p.ProductId == productId /*&& p.CartId == cart.Id*/);

            //if (cartDetail == null)
            //{
            //    cartDetail = new CartDetail
            //    {
            //        ProductId = productId,
            //        //CartId = cart.Id; //Chưa xác định được giỏ hàng của người dùng hiện tại đang Login
            //        CartId = Guid.Parse("48632772-2D15-4EA2-A863-32E317E4A3D5"),
            //        Quantity = quantity
            //    };
            //    if(_cartDetailService.CreateCartDetail(cartDetail))
            //        return RedirectToAction("Cart");
            //}
            //else
            //{
            //    cartDetail.Quantity += quantity;
            //    if(_cartDetailService.UpdateCartDetail(cartDetail))
            //        return RedirectToAction("Cart");

            //}
            return RedirectToAction("ProductDetail", "Home", new { id = productId });
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
        public IActionResult Delete(Guid id)
        {
            //var cartDetail = _cartDetailService.GetCartDetailById(id); // Lấy ra sản phẩm trong giỏ hàng mà user định xóa
            //var cartDetails = SessionService.GetObjFromSession(HttpContext.Session, "Delete"); //Lấy dữ liệu từ Session
            //if (cartDetails.Count == 0)
            //{
            //    cartDetails.Add(cartDetail);
            //    SessionService.SetObjToJson(HttpContext.Session, "Delete", cartDetails);
            //}
            //else
            //{
            //    if (SessionService.CheckProductInCart(cartDetail.ProductId, cartDetails))
            //    {
            //        var check = cartDetails.FirstOrDefault(p => p.ProductId == cartDetail.ProductId);
            //        cartDetails.Remove(check);
            //        cartDetail.Quantity += check.Quantity;
            //        cartDetails.Add(cartDetail);
            //        SessionService.SetObjToJson(HttpContext.Session, "Delete", cartDetails);
            //    }
            //    else
            //    {
            //        cartDetails.Add(cartDetail);
            //        SessionService.SetObjToJson(HttpContext.Session, "Delete", cartDetails);
            //    }
            //}

            //if (_cartDetailService.DeleteCartDetail(id))
            //{
            //    var delete = "The product that you have removed in the cart is only valid for 60 seconds!";
            //    TempData["Delete"] = delete;
            //    return RedirectToAction("Cart", new { delete });
            //}
            return HttpNotFound();
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
