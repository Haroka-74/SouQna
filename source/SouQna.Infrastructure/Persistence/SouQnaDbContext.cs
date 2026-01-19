using Microsoft.EntityFrameworkCore;
using SouQna.Domain.Aggregates.CartAggregate;
using SouQna.Domain.Aggregates.UserAggregate;
using SouQna.Domain.Aggregates.ProductAggregate;
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
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(typeof(SouQnaDbContext).Assembly);
    }
}