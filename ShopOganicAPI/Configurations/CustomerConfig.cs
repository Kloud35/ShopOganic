using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOganicAPI.Models;

namespace ShopOganicAPI.Configurations
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(p => p.CustomerID);
            builder.Property(p => p.FullName).HasColumnType("nvarchar(150)");
            builder.Property(p => p.Email).HasColumnType("varchar(150)");
            builder.Property(p => p.Password).HasColumnType("varchar(150)");
            builder.Property(p => p.PhoneNumber).HasColumnType("varchar(10)").IsRequired(false); ;
            builder.Property(p => p.ImageUrl).HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(p => p.Gender).HasColumnType("bit");
            builder.Property(p => p.CreatedDate).HasColumnType("datetime");
            builder.Property(p => p.LastLogin).HasColumnType("datetime");
            builder.Property(p => p.Address).HasColumnType("nvarchar(max)").IsRequired(false); ;
            builder.Property(p => p.Status).HasColumnType("int");

        }
    }
}
