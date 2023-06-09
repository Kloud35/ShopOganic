﻿using System.ComponentModel.DataAnnotations;

namespace ShopOganicAPI.Models
{
    public class Category
    {
        public Guid CategoryID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên thể loại.")]
        public string CategoryName { get; set; }
        public int? Status { get; set; }
        public int? Published { get; set; }
        public virtual IQueryable<Post>? Posts { get; set; }
        public virtual IQueryable<Product>? Products { get; set; }
    }
}
