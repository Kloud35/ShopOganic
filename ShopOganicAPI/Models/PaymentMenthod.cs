using System.ComponentModel.DataAnnotations;

namespace ShopOganicAPI.Models
{
	public class PaymentMenthod
	{
		public Guid PaymentMenthodID { get; set; }
		[Required(ErrorMessage = "Không được để trống !")]
		[StringLength(50)]
		public string PaymentMenthodName { get; set; }
		[Required(ErrorMessage = "Không được để trống !")]
		public int Status { get; set; }
		public virtual IQueryable<Bill> Bill { get; set; }
	}
}
