using Microsoft.EntityFrameworkCore;
using SouQna.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SouQna.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.ToTable("orders");
            builder.Property(o => o.Id).HasColumnName("id");
            builder.Property(o => o.Id).ValueGeneratedNever();
            builder.Property(o => o.Total).HasColumnName("total");
            builder.Property(o => o.Status).HasColumnName("status");
            builder.Property(o => o.UserId).HasColumnName("user_id");
            builder.Property(o => o.CreatedAt).HasColumnName("created_at");
            builder.Property(o => o.ShippedAt).HasColumnName("shipped_at");
            builder.Property(o => o.ConfirmedAt).HasColumnName("confirmed_at");
            builder.Property(o => o.DeliveredAt).HasColumnName("delivered_at");
            builder.Property(o => o.CancelledAt).HasColumnName("cancelled_at");
            builder.Property(o => o.OrderNumber).HasColumnName("order_number");
            builder.Property(o => o.ShippingCity).HasColumnName("shipping_city");
            builder.Property(o => o.ShippingFullName).HasColumnName("shipping_full_name");
            builder.Property(o => o.ShippingAddressLine).HasColumnName("shipping_address_line");
            builder.Property(o => o.ShippingPhoneNumber).HasColumnName("shipping_phone_number");
            builder.HasOne(o => o.User).WithMany(u => u.Orders).HasForeignKey(o => o.UserId).IsRequired();
            builder.HasMany(o => o.OrderItems).WithOne(oi => oi.Order).HasForeignKey(oi => oi.OrderId).IsRequired();
        }
    }
}