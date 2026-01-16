using Microsoft.EntityFrameworkCore;
using SouQna.Domain.Aggregates.CategoryAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SouQna.Infrastructure.Persistence.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Id)
                .ValueGeneratedNever();

            builder
                .HasIndex(c => c.Name)
                .IsUnique();

            builder
                .HasOne(c => c.Parent)
                .WithMany(c => c.Subcategories)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Navigation(c => c.Subcategories)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            builder
                .Navigation(c => c.Products)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}