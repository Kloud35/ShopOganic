namespace ShopOganicAPI.Models
{
    public class Category
    {
        public Guid CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int Status { get; set; }
        public int Published { get; set; }
        public virtual IQueryable<Post> Posts { get; set; }
        public virtual IQueryable<CategoryDetail> CategoryDetails { get; set; }
    }
}
