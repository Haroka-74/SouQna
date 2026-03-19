using SouQna.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SouQna.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasColumnName("id");
            builder.Property(o => o.Id).ValueGeneratedNever();

            builder.Property(o => o.UserId).HasColumnName("user_id");
            builder.Property(o => o.OrderNumber).HasColumnName("order_number");
            builder.Property(o => o.ShippingFullName).HasColumnName("shipping_full_name");
            builder.Property(o => o.ShippingPhoneNumber).HasColumnName("shipping_phone_number");
            builder.Property(o => o.ShippingCity).HasColumnName("shipping_city");
            builder.Property(o => o.ShippingAddressLine).HasColumnName("shipping_address_line");
            builder.Property(o => o.Total).HasColumnName("total");
            builder.Property(o => o.OrderStatus).HasColumnName("order_status");
            builder.Property(o => o.CreatedAt).HasColumnName("created_at");
            builder.Property(o => o.ConfirmedAt).HasColumnName("confirmed_at");
            builder.Property(o => o.ShippedAt).HasColumnName("shipped_at");
            builder.Property(o => o.DeliveredAt).HasColumnName("delivered_at");
            builder.Property(o => o.CancelledAt).HasColumnName("cancelled_at");

            builder
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .IsRequired();

            builder
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .IsRequired();

            builder
                .HasMany(o => o.Payments)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId)
                .IsRequired();
        }
    }
}