using System.ComponentModel.DataAnnotations;
using ShopOganic.Helper;

namespace ShopOganicAPI.Models
{
    public class Voucher
    {
		public Guid VoucherID { get; set; }
		[Required(ErrorMessage = "Phải nhập Name voucher")]
		/*        [RegularExpression("^[a-zA-Z0-9_]*$", ErrorMessage = "Địa chỉ không được chứa ký tự đặc biệt")]
		*/
		public string VoucherName { get; set; }
		[Required(ErrorMessage = " Phải nhập phần trăm giảm")]
		[DataType(DataType.PhoneNumber)]
		public decimal PercentDiscount { get; set; }
		[Required(ErrorMessage = " Phải nhập thời gian bắt đầu")]
		[DataType(DataType.DateTime)]
		public DateTime? TimeStart { get; set; }

		[Required(ErrorMessage = " Phải nhập nhập thời gian kết thúc")]
		[DataType(DataType.DateTime)]
		[GreaterThan("TimeStart", ErrorMessage = "Thời gian kết thúc lớn hơn thời gian bắt đầu")]

		public DateTime? TimeEnd { get; set; }
		public int? Status { get; set; }
		public string? Description { get; set; }
		public virtual IQueryable<Bill>? Bills { get; set; }
	}
}
