using Microsoft.EntityFrameworkCore;
using SouQna.Domain.Aggregates.UserAggregate;
using SouQna.Domain.Aggregates.CategoryAggregate;

namespace SouQna.Infrastructure.Persistence
{
    public class SouQnaDbContext(
        DbContextOptions<SouQnaDbContext> options
    ) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(typeof(SouQnaDbContext).Assembly);
    }
}