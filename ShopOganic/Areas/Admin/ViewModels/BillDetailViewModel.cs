using ShopOganicAPI.Models;

namespace ShopOganic.Areas.Admin.ViewModels
{
    public class BillDetailViewModel
    {
        public Bill Bill { get; set; }
        public List<BillDetail> BillDetails { get; set; }
    }
}
