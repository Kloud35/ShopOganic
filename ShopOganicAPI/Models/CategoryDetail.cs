namespace ShopOganicAPI.Models
{
    public class CategoryDetail
    {
        public Guid CategoryDetailID { get; set; }
        public Guid CategoryID { get; set; }
        public Guid ProductID { get; set; }
        public int? Quantity { get; set; }
        public virtual Category? Category { get; set; }
        public virtual Product? Product { get; set; }
    }
}
