using System.ComponentModel.DataAnnotations;

namespace ShopOganicAPI.Models
{
	public class Product
	{
		public Guid ProductID { get; set; }
		public Guid CategoryID { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập tên sản phẩm.")]

		public string ProductName { get; set; }
		public string? Description { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập giá sản phẩm.")]
		public decimal Price { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập số lượng sản phẩm.")]
		public int Quantity { get; set; }
		public DateTime? CreatedDate { get; set; }
		public string? ImageUrl { get; set; }
		public int? Status { get; set; }
		public virtual IQueryable<BillDetail>? BillDetails { get; set; }
		public virtual IQueryable<CartDetail>? CartDetails { get; set; }
		public virtual Category? Category { get; set; }
	}
}
