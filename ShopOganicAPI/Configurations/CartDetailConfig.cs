using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOganicAPI.Models;

namespace ShopOganicAPI.Configurations
{
    public class CartDetailConfig : IEntityTypeConfiguration<CartDetail>
    {
        public void Configure(EntityTypeBuilder<CartDetail> builder)
        {
            builder.HasKey(p => p.CartDetailID);
            builder.Property(p=>p.Quantity).HasColumnType("int");
            builder.HasOne(p => p.Cart).WithMany(p => p.CartDetails).HasForeignKey(p => p.CustomerID);
            builder.HasOne(p => p.Product).WithMany(p => p.CartDetails).HasForeignKey(p => p.ProductID);
        }
    }
}
