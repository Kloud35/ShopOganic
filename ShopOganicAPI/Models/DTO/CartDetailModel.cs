namespace ShopOganicAPI.Models.DTO
{
    public class CartDetailModel
    {

        public Guid productId { get; set; }
        public Guid customerId { get; set; }
        public int quantity { get; set; }
    }
}
