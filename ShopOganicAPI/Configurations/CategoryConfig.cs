using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOganicAPI.Models;

namespace ShopOganicAPI.Configurations
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(p=>p.CategoryID);
            builder.Property(p=>p.CategoryName).HasColumnType("nvarchar(150)");
            builder.Property(p=>p.Status).HasColumnType("int");
            builder.Property(p=>p.Published).HasColumnType("int");
        }
    }
}
