using Microsoft.EntityFrameworkCore;
using SouQna.Domain.Aggregates.CartAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SouQna.Infrastructure.Persistence.Configuration
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder
                .HasKey(ci => ci.Id);

            builder
                .Property(ci => ci.Id)
                .ValueGeneratedNever();
        }
    }
}