using SouQna.Domain.Aggregates.UserAggregate;
using SouQna.Domain.Aggregates.CategoryAggregate;

namespace SouQna.Application.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Category> Categories { get; }
        Task SaveChangesAsync();
    }
}