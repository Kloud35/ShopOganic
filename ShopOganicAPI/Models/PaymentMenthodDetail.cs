namespace ShopOganicAPI.Models
{
    public class PaymentMenthodDetail
    {
        public Guid PaymentMenthodDetailID { get; set; }
        public Guid PaymentMenthodID { get; set; }
        public Guid BillID { get; set; }
        public string? Description { get; set; }
        public virtual PaymentMenthod? PaymentMenthod { get; set; }
        public virtual Bill? Bill { get; set; }
    }
}
