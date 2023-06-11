using System.ComponentModel.DataAnnotations;

namespace ShopOganicAPI.Models
{
    public class Post
    {
		public Guid PostID { get; set; }
		[Required]
		public Guid AccountID { get; set; }
		[Required]
		public Guid CategoryID { get; set; }

		[Required(ErrorMessage = "Phải nhập tiêu đề")]
		public string Title { get; set; }
		[Required(ErrorMessage = "Phải nhập mô tả")]

		public string Contents { get; set; }
		[Required(ErrorMessage = "Chọn ảnh")]
		public string ImageUrl { get; set; }
		public string? Alias { get; set; }
		public string? Author { get; set; }
		public string? Tags { get; set; }
		public bool IsHot { get; set; }
		public DateTime? CreatedDate { get; set; }
		public virtual Account? Account { get; set; }
		public virtual Category? Category { get; set; }
	}
}
