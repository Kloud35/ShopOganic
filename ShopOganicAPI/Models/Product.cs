namespace ShopOganicAPI.Models
{
    public class Product
    {
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ImageUrl { get; set; }
        public int Status { get; set; }
        public virtual IQueryable<CategoryDetail> CategoryDetails { get; set; }
        public virtual IQueryable<BillDetail> BillDetails { get; set; }
        public virtual IQueryable<CartDetail> CartDetails { get; set; }
    }
}
