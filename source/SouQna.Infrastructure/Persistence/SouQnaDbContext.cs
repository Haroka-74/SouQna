using SouQna.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SouQna.Infrastructure.Persistence
{
    public class SouQnaDbContext(
        DbContextOptions<SouQnaDbContext> options
    ) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(typeof(SouQnaDbContext).Assembly);
    }
}