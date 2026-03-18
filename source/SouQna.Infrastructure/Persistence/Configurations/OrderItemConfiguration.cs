using SouQna.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SouQna.Infrastructure.Persistence.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("order_items");

            builder.HasKey(oi => oi.Id);
            builder.Property(oi => oi.Id).HasColumnName("id");
            builder.Property(oi => oi.Id).ValueGeneratedNever();

            builder.Property(oi => oi.OrderId).HasColumnName("order_id");
            builder.Property(oi => oi.ProductId).HasColumnName("product_id");
            builder.Property(oi => oi.ItemName).HasColumnName("item_name");
            builder.Property(oi => oi.ItemImage).HasColumnName("item_image");
            builder.Property(oi => oi.ItemPrice).HasColumnName("item_price");
            builder.Property(oi => oi.ItemQuantity).HasColumnName("item_quantity");
        }
    }
}