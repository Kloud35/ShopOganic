using System.ComponentModel.DataAnnotations;

namespace ShopOganicAPI.Models
{
    public class ShipMenthod
    {
		public Guid ShipMenthodID { get; set; }
		[Required(ErrorMessage = "Yêu cầu người dùng nhập địa chỉ.")]
		[StringLength(100, ErrorMessage = "Hình thức thanh toán không được vượt quá 100 kí tự.")]
		[RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Hình thức thanh toán không được chứa kí tự đặc biệt.")]
		public string ShippingMenthodName { get; set; }
		[Required(ErrorMessage = "Yêu cầu người dùng nhập giá vận chuyển.")]
		[RegularExpression("^[0-9.]*$", ErrorMessage = "Giá vận chuyển chỉ được nhập số")]
		public decimal ShipPrice { get; set; }
		[Required(ErrorMessage = "Yêu cầu người dùng nhập trạng thái.")]
		public int? Status { get; set; }
		public virtual IQueryable<Bill>? Bills { get; set; }
	}
}
