using SouQna.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SouQna.Infrastructure.Persistence.Configurations
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("inventories");

            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).HasColumnName("id");
            builder.Property(i => i.Id).ValueGeneratedNever();

            builder.Property(i => i.ProductId).HasColumnName("product_id");
            builder.Property(i => i.Quantity).HasColumnName("quantity");
        }
    }
}