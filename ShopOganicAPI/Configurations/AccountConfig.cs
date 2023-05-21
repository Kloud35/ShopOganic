using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOganicAPI.Models;
using System;

namespace ShopOganicAPI.Configurations
{
    public class AccountConfig : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(p => p.AccountID);
            builder.Property(p => p.AccountName).HasColumnType("nvarchar(150)");
            builder.Property(p => p.ImageUrl).HasColumnType("nvarchar(max)");
            builder.Property(p => p.Email).HasColumnType("varchar(max)");
            builder.Property(p => p.Password).HasColumnType("varchar(max)");
            builder.Property(p => p.FullName).HasColumnType("nvarchar(max)");
            builder.Property(p => p.PhoneNumber).HasColumnType("varchar(10)");
            builder.Property(p => p.Gender).HasColumnType("bit");
            builder.Property(p => p.IsActive).HasColumnType("bit");
            builder.Property(p => p.DateOfBirth).HasColumnType("datetime");
            builder.Property(p => p.CreatedDate).HasColumnType("datetime");
            builder.Property(p => p.LastLogin).HasColumnType("datetime");
            builder.HasOne(p => p.Role).WithMany(p => p.Accounts).HasForeignKey(p => p.RoleID).IsRequired(false);
        }
    }
}
