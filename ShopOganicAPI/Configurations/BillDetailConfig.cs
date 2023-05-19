using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOganicAPI.Models;

namespace ShopOganicAPI.Configurations
{
    public class BillDetailConfig : IEntityTypeConfiguration<BillDetail>
    {
        public void Configure(EntityTypeBuilder<BillDetail> builder)
        {
            builder.HasKey(p => p.BillDetailID);
            builder.Property(p => p.Quantity).HasColumnType("int");
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.Property(p => p.TotalMoney).HasColumnType("decimal(18,2)");
            builder.HasOne(p=>p.Bill).WithMany(p=>p.BillDetails).HasForeignKey(p=>p.BillID);
            builder.HasOne(p=>p.Product).WithMany(p=>p.BillDetails).HasForeignKey(p=>p.ProductID);
        }
    }
}
