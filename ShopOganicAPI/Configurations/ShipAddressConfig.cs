using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOganicAPI.Models;

namespace ShopOganicAPI.Configurations
{
    public class ShipAddressConfig : IEntityTypeConfiguration<ShipAddress>
    {
        public void Configure(EntityTypeBuilder<ShipAddress> builder)
        {
            builder.HasKey(p => p.ShipAddressID);
            builder.Property(p=>p.Address).HasColumnType("nvarchar(max)");
        }
    }
}
