using SouQna.Infrastructure.Entities;
using SouQna.Infrastructure.Interfaces;

namespace SouQna.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SouQnaDbContext _context;

        public IRepository<User> Users { get; private set; }
        public IRepository<Product> Products { get; private set; }
        public IRepository<Cart> Carts { get; private set; }
        public IRepository<CartItem> CartItems { get; private set; }

        public UnitOfWork(SouQnaDbContext context)
        {
            _context = context;

            Users = new Repository<User>(_context);
            Products = new Repository<Product>(_context);
            Carts = new Repository<Cart>(_context);
            CartItems = new Repository<CartItem>(_context);
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public async ValueTask DisposeAsync() => await _context.DisposeAsync();
    }
}