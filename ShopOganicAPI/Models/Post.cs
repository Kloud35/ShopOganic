namespace ShopOganicAPI.Models
{
    public class Post
    {
        public Guid PostID { get; set; }
        public Guid AccountID { get; set; }
        public Guid CategoryID { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public string ImageUrl { get; set; }
        public string Alias { get; set; }
        public string Author { get; set; }
        public string Tags { get; set; }
        public bool IsHot { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual Account Account { get; set; }
        public virtual Category Category { get; set; }
    }
}
