using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOganicAPI.Models;

namespace ShopOganicAPI.Configurations
{
    public class BillConfig : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.HasKey(p => p.BillID);
            builder.Property(p => p.BillCode).HasColumnType("varchar(50)").IsRequired();
            builder.Property(p => p.PaymentMenthod).HasColumnType("nvarchar(150)");
            builder.Property(p => p.TotalMoney).HasColumnType("decimal(18,2)");
            builder.Property(p => p.ReceiverName).HasColumnType("nvarchar(150)");
            builder.Property(p => p.CustomerPhone).HasColumnType("varchar(10)");
            builder.Property(p => p.AddressDelivery).HasColumnType("nvarchar(max)");
            builder.Property(p => p.CreatedDate).HasColumnType("datetime");
            builder.Property(p => p.Status).HasColumnType("int");
            builder.HasOne(p => p.Customer).WithMany(p => p.Bills).HasForeignKey(p => p.CustomerID);
            builder.HasOne(p => p.Voucher).WithMany(p => p.Bills).HasForeignKey(p => p.VoucherID);
            builder.HasOne(p => p.ShipAddress).WithMany(p => p.Bills).HasForeignKey(p => p.ShipAddressID);
            builder.HasOne(p => p.ShipMenthod).WithMany(p => p.Bills).HasForeignKey(p => p.ShipMenthodID);
        }
    }
}
