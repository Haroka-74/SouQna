using SouQna.Domain.Entities;
using SouQna.Application.Interfaces;

namespace SouQna.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SouQnaDbContext _context;

        public IRepository<User> Users { get; private set; }
        public IRepository<Product> Products { get; private set; }
        public IRepository<Cart> Carts { get; private set; }
        public IRepository<CartItem> CartItems { get; private set; }
        public IRepository<Inventory> Inventories { get; private set; }
        public IRepository<Order> Orders { get; private set; }
        public IRepository<OrderItem> OrderItems { get; private set; }
        public IRepository<Payment> Payments { get; private set; }

        public UnitOfWork(SouQnaDbContext context)
        {
            _context = context;

            Users = new Repository<User>(_context);
            Products = new Repository<Product>(_context);
            Carts = new Repository<Cart>(_context);
            CartItems = new Repository<CartItem>(_context);
            Inventories = new Repository<Inventory>(_context);
            Orders = new Repository<Order>(_context);
            OrderItems = new Repository<OrderItem>(_context);
            Payments = new Repository<Payment>(_context);
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public async ValueTask DisposeAsync() => await _context.DisposeAsync();
    }
}