using Microsoft.EntityFrameworkCore;
using SouQna.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SouQna.Infrastructure.Persistence.Configuration
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder
                .HasKey(r => r.Id);

            builder
                .Property(r => r.Id)
                .ValueGeneratedNever();

            builder
                .HasIndex(r => r.Token)
                .IsUnique();
        }
    }
}