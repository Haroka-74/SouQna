using SouQna.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SouQna.Infrastructure.Persistence.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("user_roles");

            builder.HasKey(ur => ur.Id);
            builder.Property(ur => ur.Id).HasColumnName("id");
            builder.Property(ur => ur.Id).ValueGeneratedNever();

            builder.Property(ur => ur.UserId).HasColumnName("user_id");
            builder.Property(ur => ur.RoleId).HasColumnName("role_id");
        }
    }
}