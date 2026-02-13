using Microsoft.EntityFrameworkCore;
using SouQna.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SouQna.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.ToTable("products");
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(p => p.Name).HasColumnName("name");
            builder.Property(p => p.Image).HasColumnName("image");
            builder.Property(p => p.Price).HasColumnName("price");
            builder.Property(p => p.CreatedAt).HasColumnName("created_at");
            builder.Property(p => p.Description).HasColumnName("description");
        }
    }
}