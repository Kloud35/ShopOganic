namespace ShopOganicAPI.Models
{
    public class BillDetail
    {
        public Guid BillDetailID { get; set; }
        public Guid BillID { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalMoney { get; set; }
        public virtual Bill Bill { get; set; }
        public virtual Product Product { get; set; }
    }
}
