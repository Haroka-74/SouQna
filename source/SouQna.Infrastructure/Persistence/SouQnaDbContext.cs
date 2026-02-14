using Microsoft.EntityFrameworkCore;
using SouQna.Infrastructure.Entities;

namespace SouQna.Infrastructure.Persistence
{
    public class SouQnaDbContext(
        DbContextOptions<SouQnaDbContext> options
    ) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(typeof(SouQnaDbContext).Assembly);
    }
}