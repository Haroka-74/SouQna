using Microsoft.EntityFrameworkCore;
using SouQna.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SouQna.Infrastructure.Persistence.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(ci => ci.Id);
            builder.ToTable("cart_items");
            builder.Property(ci => ci.Id).HasColumnName("id");
            builder.Property(ci => ci.Id).ValueGeneratedNever();
            builder.Property(ci => ci.CartId).HasColumnName("cart_id");
            builder.Property(ci => ci.Quantity).HasColumnName("quantity");
            builder.Property(ci => ci.ProductId).HasColumnName("product_id");
            builder.Property(ci => ci.PriceAtAddition).HasColumnName("price_at_addition");
        }
    }
}