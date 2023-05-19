using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOganicAPI.Models;

namespace ShopOganicAPI.Configurations
{
    public class ShipMenthodConfig : IEntityTypeConfiguration<ShipMenthod>
    {
        public void Configure(EntityTypeBuilder<ShipMenthod> builder)
        {
            builder.HasKey(p => p.ShipMenthodID);
            builder.Property(p=>p.ShippingMenthodName).HasColumnType("nvarchar(150)");
            builder.Property(p=>p.ShipPrice).HasColumnType("decimal(18,2)");
            builder.Property(p=>p.Status).HasColumnType("int");
        }
    }
}
