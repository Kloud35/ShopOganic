namespace ShopOganicAPI.Models
{
    public class Role
    {
        public Guid RoleID { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public IQueryable<Account> Accounts { get; set; }
    }
}
