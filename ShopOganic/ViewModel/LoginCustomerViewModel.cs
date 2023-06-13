using System.ComponentModel.DataAnnotations;

namespace ShopOganic.ViewModel
{
    public class LoginCustomerViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập Họ Tên")]
        public string FullName { get; set; }

        [EmailAddress(ErrorMessage = "Bạn nhập không đúng định dạng email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập password")]
        [StringLength(50)]
        public string Password { get; set; }
        [StringLength(50)]
        [Compare("Password", ErrorMessage = "Password không trùng nhau")]
        public string RePassword { get; set; }

    }
}
