using System.ComponentModel.DataAnnotations;

namespace ShopOganicAPI.Models
{
    public class ShipAddress
    {
		public Guid ShipAddressID { get; set; }
		[Required(ErrorMessage = "Yêu cầu người dùng nhập địa chỉ.")]
		[StringLength(300, ErrorMessage = "Địa chỉ không được vượt quá 300 kí tự.")]
		[RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Địa chỉ không được chứa kí tự đặc biệt.")]
		public string? Address { get; set; }
		public virtual IQueryable<Bill>? Bills { get; set; }
	}
}
