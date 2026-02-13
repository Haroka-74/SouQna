using SouQna.Infrastructure.Entities;

namespace SouQna.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<User> Users { get; }
        Task SaveChangesAsync();
    }
}