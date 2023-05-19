using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOganicAPI.Models;

namespace ShopOganicAPI.Configurations
{
    public class LocationConfig : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(p => p.LoactionID);
            builder.Property(p => p.City).HasColumnType("nvarchar(50)");
            builder.Property(p => p.State).HasColumnType("nvarchar(50)");
        }
    }
}
