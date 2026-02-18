using Microsoft.EntityFrameworkCore;
using SouQna.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SouQna.Infrastructure.Persistence.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);
            builder.ToTable("payments");
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(p => p.Amount).HasColumnName("amount");
            builder.Property(p => p.Status).HasColumnName("status");
            builder.Property(p => p.OrderId).HasColumnName("order_id");
            builder.Property(p => p.IntentId).HasColumnName("intent_id");
            builder.Property(p => p.CreatedAt).HasColumnName("created_at");
            builder.Property(p => p.TransactionId).HasColumnName("transaction_id");
            builder.HasOne(p => p.Order).WithOne(o => o.Payment).HasForeignKey<Payment>(p => p.OrderId);
        }
    }
}