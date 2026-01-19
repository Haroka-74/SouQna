using SouQna.Application.Interfaces;
using SouQna.Domain.Aggregates.CartAggregate;
using SouQna.Domain.Aggregates.UserAggregate;
using SouQna.Domain.Aggregates.ProductAggregate;
using SouQna.Domain.Aggregates.CategoryAggregate;

namespace SouQna.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SouQnaDbContext _context;

        public IRepository<User> Users { get; private set; }
        public IRepository<Category> Categories { get; private set; }
        public IRepository<Product> Products { get; private set; }
        public IRepository<Cart> Carts { get; private set; }

        public UnitOfWork(SouQnaDbContext context)
        {
            _context = context;
            Users = new Repository<User>(_context);
            Categories = new Repository<Category>(_context);
            Products = new Repository<Product>(_context);
            Carts = new Repository<Cart>(_context);
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public async ValueTask DisposeAsync() => await _context.DisposeAsync();
    }
}