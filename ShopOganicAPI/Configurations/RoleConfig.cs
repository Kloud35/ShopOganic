using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOganicAPI.Models;

namespace ShopOganicAPI.Configurations
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(p => p.RoleID);
            builder.Property(p => p.RoleCode).HasColumnType("varchar(50)");
            builder.Property(p => p.RoleName).HasColumnType("nvarchar(50)");
            builder.Property(p => p.Description).HasColumnType("nvarchar(max)");
        }
    }
}
