namespace ShopOganicAPI.Models
{
    public class ShipMenthod
    {
        public Guid ShipMenthodID { get; set; }
        public string ShippingMenthodName { get; set; }
        public decimal ShipPrice { get; set; }
        public int Status { get; set; }
        public virtual IQueryable<Bill> Bills { get; set; }
    }
}
