using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOganicAPI.Models;

namespace ShopOganicAPI.Configurations
{
    public class PaymentMenthodDetailConfig : IEntityTypeConfiguration<PaymentMenthodDetail>
    {
        public void Configure(EntityTypeBuilder<PaymentMenthodDetail> builder)
        {
            builder.HasKey(p => p.PaymentMenthodDetailID);
            builder.Property(p=>p.Description).HasColumnType("nvarchar(max)");
            builder.HasOne(p => p.PaymentMenthod).WithMany(p => p.PaymentMenthodDetails).HasForeignKey(p => p.PaymentMenthodID);
            builder.HasOne(p => p.Bill).WithMany(p => p.PaymentMenthodDetails).HasForeignKey(p => p.BillID);

        }
    }
}
