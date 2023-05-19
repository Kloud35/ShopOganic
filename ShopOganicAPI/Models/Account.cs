namespace ShopOganicAPI.Models
{
    public class Account
    {
        public Guid AccountID { get; set; }
        public Guid RoleID { get; set; }
        public string AccountName { get; set; }
        public string ImageUrl { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastLogin { get; set; }
        public bool Active { get; set; }

        public virtual Role Role { get; set; }
        public virtual IQueryable<Post> Posts { get; set; }
    }
}
