using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOganicAPI.Models;

namespace ShopOganicAPI.Configurations
{
    public class CategoryDetailConfig : IEntityTypeConfiguration<CategoryDetail>
    {
        public void Configure(EntityTypeBuilder<CategoryDetail> builder)
        {
            builder.HasKey(p => p.CategoryDetailID);
            builder.Property(p=>p.Quantity).HasColumnType("int");
            builder.HasOne(p=>p.Product).WithMany(p=>p.CategoryDetails).HasForeignKey(p=>p.ProductID);
            builder.HasOne(p=>p.Category).WithMany(p=>p.CategoryDetails).HasForeignKey(p=>p.CategoryID);
        }
    }
}
