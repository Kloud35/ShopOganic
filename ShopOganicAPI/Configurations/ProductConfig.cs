using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOganicAPI.Models;

namespace ShopOganicAPI.Configurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductID);
            builder.Property(p => p.ProductName).HasColumnType("nvarchar(max)");
            builder.Property(p => p.Description).HasColumnType("nvarchar(max)");
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.Property(p => p.Quantity).HasColumnType("int");
            builder.Property(p => p.CreatedDate).HasColumnType("datetime");
            builder.Property(p => p.ImageUrl).HasColumnType("nvarchar(150)");
            builder.Property(p => p.Status).HasColumnType("int");
            builder.HasOne(p=>p.Category).WithMany(p => p.Products).HasForeignKey(p => p.CategoryID);
        }
    }
}
