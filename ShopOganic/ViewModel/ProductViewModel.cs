//using ASM_CSharp4_Linhtnph20247.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShopOganic.ViewModel
{
    public class ProductViewModel
    {

        public int QuantityAddToCart { get; set; }
        public Guid ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
        public int Status { get; set; }
    }
}
