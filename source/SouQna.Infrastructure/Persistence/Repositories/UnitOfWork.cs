using SouQna.Infrastructure.Entities;
using SouQna.Infrastructure.Interfaces;

namespace SouQna.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SouQnaDbContext _context;

        public IRepository<User> Users { get; private set; }

        public UnitOfWork(SouQnaDbContext context)
        {
            _context = context;

            Users = new Repository<User>(_context);
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public async ValueTask DisposeAsync() => await _context.DisposeAsync();
    }
}