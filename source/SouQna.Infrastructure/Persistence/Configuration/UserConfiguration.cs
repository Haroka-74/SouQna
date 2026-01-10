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
                .HasIndex(u => u.Email)
                .IsUnique();

            builder
                .HasIndex(u => u.EmailConfirmationToken)
                .IsUnique();
        }
    }
}