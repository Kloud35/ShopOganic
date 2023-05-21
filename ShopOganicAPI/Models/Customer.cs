namespace ShopOganicAPI.Models
{
    public class Customer
    {
        public Guid CustomerID { get; set; }
        public string? FullName { get; set; } 
        public string Email { get; set; } 
        public string Password { get; set; } 
        public string? PhoneNumber { get; set; } = null!;
        public string? ImageUrl { get; set; } = null!;
        public bool Gender { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastLogin { get; set; }
        public string? Address { get; set; } = null!;
        public int? Status { get; set; }
        public virtual IQueryable<Bill>? Bills { get; set; } = null!;
    }
}
