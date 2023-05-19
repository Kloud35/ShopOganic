namespace ShopOganicAPI.Models
{
    public class CartDetail
    {
        public Guid CartDetailID { get; set; }
        public Guid CustomerID { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual Product Product { get; set; }

    }
}
