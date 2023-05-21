namespace ShopOganicAPI.Models
{
    public class ShipAddress
    {
        public Guid ShipAddressID { get; set; }
        public string? Address { get; set; }
        public virtual IQueryable<Bill>? Bills { get; set; }
    }
}
