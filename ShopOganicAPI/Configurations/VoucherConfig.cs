using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOganicAPI.Models;

namespace ShopOganicAPI.Configurations
{
    public class VoucherConfig : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.HasKey(p => p.VoucherID);
            builder.Property(p=>p.VoucherName).HasColumnType("nvarchar(150)");
            builder.Property(p=>p.PercentDiscount).HasColumnType("decimal(18,2)");
            builder.Property(p=>p.TimeStart).HasColumnType("datetime");
            builder.Property(p=>p.TimeEnd).HasColumnType("datetime");
            builder.Property(p=>p.Status).HasColumnType("int");
            builder.Property(p=>p.Description).HasColumnType("nvarchar(max)");
        }
    }
}
