namespace ShopOganicAPI.Models
{
    public class Account
    {
        public Guid AccountID { get; set; }
        public Guid RoleID { get; set; }
        public string? AccountName { get; set; } = null!;
        public string? ImageUrl { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? FullName { get; set; } = null!;
        public string? PhoneNumber { get; set; } = null!;
        public string? Address { get; set; } = null!;
        public bool Gender { get; set; } 
        public DateTime? DateOfBirth { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsActive { get; set; }

        public virtual Role? Role { get; set; }
        public virtual IQueryable<Post>? Posts { get; set; }
    }
}
