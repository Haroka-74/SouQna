using Microsoft.EntityFrameworkCore;
using SouQna.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SouQna.Infrastructure.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(u => u.Id);

            builder
                .Property(u => u.Id)
                .ValueGeneratedNever();

            builder
                .HasIndex(u => u.Email)
                .IsUnique();

            builder
                .HasIndex(u => u.EmailConfirmationToken)
                .IsUnique();

            builder
                .HasMany(u => u.RefreshTokens)
                .WithOne(t => t.User)
                .HasForeignKey(r => r.UserId)
                .IsRequired();

            builder
                .Navigation(u => u.RefreshTokens)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}