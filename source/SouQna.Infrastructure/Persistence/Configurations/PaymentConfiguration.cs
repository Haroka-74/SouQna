using SouQna.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SouQna.Infrastructure.Persistence.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("payments");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.Id).ValueGeneratedNever();

            builder.Property(p => p.OrderId).HasColumnName("order_id");
            builder.Property(p => p.IntentionOrderId).HasColumnName("intention_order_id");
            builder.Property(p => p.CheckoutUrl).HasColumnName("checkout_url");
            builder.Property(p => p.RetryCount).HasColumnName("retry_count");
            builder.Property(p => p.PaymentStatus).HasColumnName("payment_status");
            builder.Property(p => p.CreatedAt).HasColumnName("created_at");
            builder.Property(p => p.ExpiresAt).HasColumnName("expires_at");
        }
    }
}