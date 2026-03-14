using SouQna.Domain.Entities;

namespace SouQna.Application.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Product> Products { get; }
        Task SaveChangesAsync();
    }
}