using SouQna.Domain.Aggregates.UserAggregate;

namespace SouQna.Application.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<User> Users { get; }
        Task SaveChangesAsync();
    }
}