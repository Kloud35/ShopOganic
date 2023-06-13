namespace ShopOganicAPI.Models.DTO
{
    public class BillModel
    {
        public Bill Bill { get; set; }
        public List<BillDetail> BillDetail { get; set; }
    }
}
