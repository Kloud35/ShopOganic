namespace ShopOganicAPI.Models
{
    public class Bill
    {
        public Guid BillID { get; set; }
        public Guid CustomerID { get; set; }
        public Guid VoucherID { get; set; }
        public Guid ShipAddressID { get; set; }
        public Guid ShipMenthodID { get; set; }
        public Guid PaymentMenthodID { get; set; }
        public string? BillCode { get; set; }
        public string? PaymentMenthod { get; set; }
        public decimal? TotalMoney { get; set; }
        public string? ReceiverName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? AddressDelivery { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? Status { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual Voucher? Voucher { get; set; }
        public virtual ShipAddress? ShipAddress { get; set; }
        public virtual ShipMenthod? ShipMenthod { get; set; }
        public virtual IQueryable<BillDetail>? BillDetails { get; set; }
        public virtual PaymentMenthod? PaymentMenthods { get; set; }
    }
}
