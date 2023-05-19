namespace ShopOganicAPI.Models
{
    public class Voucher
    {
        public Guid VoucherID { get; set; }
        public string VoucherName { get; set; }
        public decimal PercentDiscount { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public virtual IQueryable<Bill> Bills { get; set; }
    }
}
