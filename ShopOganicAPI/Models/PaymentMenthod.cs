namespace ShopOganicAPI.Models
{
    public class PaymentMenthod
    {
        public Guid PaymentMenthodID { get; set; }
        public string? PaymentMenthodName { get; set; }
        public virtual IQueryable<PaymentMenthodDetail>? PaymentMenthodDetails { get; set; }
    }
}
