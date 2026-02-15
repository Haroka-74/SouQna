using Microsoft.EntityFrameworkCore;
using SouQna.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SouQna.Infrastructure.Persistence.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);
            builder.ToTable("order_items");
            builder.Property(oi => oi.Id).HasColumnName("id");
            builder.Property(oi => oi.Id).ValueGeneratedNever();
            builder.Property(oi => oi.Price).HasColumnName("price");
            builder.Property(oi => oi.OrderId).HasColumnName("order_id");
            builder.Property(oi => oi.Quantity).HasColumnName("quantity");
            builder.Property(oi => oi.ProductId).HasColumnName("product_id");
            builder.Property(oi => oi.ProductName).HasColumnName("Product_name");
            builder.Property(oi => oi.ProductImage).HasColumnName("product_image");
            builder.Property(oi => oi.ProductDescription).HasColumnName("product_description");
        }
    }
}