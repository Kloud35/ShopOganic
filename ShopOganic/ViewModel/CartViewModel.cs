//using ASM_CSharp4_Linhtnph20247.Models;

using ShopOganicAPI.Models;

namespace ShopOganic.ViewModel
{
    public class CartViewModel
    {
        public List<CartDetail> CartDetails { get; set; }
        public List<Product> Products { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
