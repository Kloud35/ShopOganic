namespace ShopOganicAPI.Models
{
    public class Cart
    {
        public Guid CustomerID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual IQueryable<CartDetail> CartDetails { get; set; }
    }
}
