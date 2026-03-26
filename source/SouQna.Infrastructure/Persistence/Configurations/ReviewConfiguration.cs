using SouQna.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SouQna.Infrastructure.Persistence.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("reviews");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).HasColumnName("id");
            builder.Property(r => r.Id).ValueGeneratedNever();

            builder.Property(r => r.UserId).HasColumnName("user_id");
            builder.Property(r => r.ProductId).HasColumnName("product_id");
            builder.Property(r => r.Rating).HasColumnName("rating");
            builder.Property(r => r.Body).HasColumnName("body");
            builder.Property(r => r.CreatedAt).HasColumnName("created_at");
        }
    }
}