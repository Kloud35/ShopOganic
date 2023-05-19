using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOganicAPI.Models;

namespace ShopOganicAPI.Configurations
{
    public class PaymentMenthodConfig : IEntityTypeConfiguration<PaymentMenthod>
    {
        public void Configure(EntityTypeBuilder<PaymentMenthod> builder)
        {
            builder.HasKey(p => p.PaymentMenthodID);
            builder.Property(p => p.PaymentMenthodName).HasColumnType("nvarchar(150)");
        }
    }
}
