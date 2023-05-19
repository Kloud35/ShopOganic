using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOganicAPI.Models;

namespace ShopOganicAPI.Configurations
{
    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.PostID);
            builder.Property(p=>p.Title).HasColumnType("nvarchar(max)");
            builder.Property(p=>p.Contents).HasColumnType("nvarchar(max)");
            builder.Property(p=>p.ImageUrl).HasColumnType("nvarchar(150)");
            builder.Property(p=>p.Alias).HasColumnType("nvarchar(max)");
            builder.Property(p=>p.Author).HasColumnType("nvarchar(150)");
            builder.Property(p=>p.Tags).HasColumnType("nvarchar(max)");
            builder.Property(p=>p.IsHot).HasColumnType("bit");
            builder.Property(p=>p.CreatedDate).HasColumnType("datetime");
            builder.HasOne(p => p.Account).WithMany(p => p.Posts).HasForeignKey(p => p.AccountID);
            builder.HasOne(p => p.Category).WithMany(p => p.Posts).HasForeignKey(p => p.CategoryID);
        }
    }
}
