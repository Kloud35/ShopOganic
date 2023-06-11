using System.ComponentModel.DataAnnotations;

namespace ShopOganicAPI.Models
{
    public class Account
    {
        public Guid AccountID { get; set; }

		[Required(ErrorMessage = "The Role field is required.")]
		public Guid RoleID { get; set; }


        [StringLength(100, ErrorMessage = "ahihi, 100 kí tự thôi")]
        public string AccountName { get; set; }

        //[Required(ErrorMessage = "The Image field is required.")]
        public string? ImageUrl { get; set; } 

        [Required(ErrorMessage = "The Password field is required.")]
        public string Password { get; set; } 

        [EmailAddress(ErrorMessage = "Mời nhập đúng định dạng Email")]
        public string Email { get; set; } 

        [StringLength(100, ErrorMessage = "ahihi, 100 kí tự thôi")]
        public string FullName { get; set; } 

        public string? PhoneNumber { get; set; } 
        public string? Address { get; set; } 
        public bool Gender { get; set; } 
        public DateTime? DateOfBirth { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsActive { get; set; }

        public virtual Role? Role { get; set; }
        public virtual IQueryable<Post>? Posts { get; set; }
    }
}
