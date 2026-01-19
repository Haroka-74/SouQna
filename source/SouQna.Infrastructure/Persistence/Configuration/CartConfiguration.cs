using Microsoft.EntityFrameworkCore;
using SouQna.Domain.Aggregates.CartAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SouQna.Infrastructure.Persistence.Configuration
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Id)
                .ValueGeneratedNever();

            builder
                .HasIndex(c => c.UserId)
                .IsUnique();

            builder
                .HasOne(c => c.User)
                .WithOne(u => u.Cart)
                .HasForeignKey<Cart>(c => c.UserId)
                .IsRequired();

            builder
                .HasMany(c => c.CartItems)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId)
                .IsRequired();

            builder
                .Navigation(c => c.CartItems)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}