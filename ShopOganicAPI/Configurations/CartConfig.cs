using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOganicAPI.Models;

namespace ShopOganicAPI.Configurations
{
    public class CartConfig : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(p => p.CustomerID);
            builder.Property(p => p.CreatedDate).HasColumnType("datetime");
            builder.Property(p => p.Description).HasColumnType("nvarchar(max)");
            builder.HasOne(p => p.Customer).WithOne(p => p.Cart);
        }
    }
}
