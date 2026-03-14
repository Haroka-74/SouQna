using SouQna.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SouQna.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.Id).ValueGeneratedNever();

            builder.Property(u => u.FirstName).HasColumnName("first_name");
            builder.Property(u => u.LastName).HasColumnName("last_name");

            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.Email).HasColumnName("email");

            builder.Property(u => u.PasswordHash).HasColumnName("password_hash");
            builder.Property(u => u.CreatedAt).HasColumnName("created_at");
        }
    }
}