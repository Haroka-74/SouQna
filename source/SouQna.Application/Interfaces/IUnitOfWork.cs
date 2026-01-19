using SouQna.Domain.Aggregates.CartAggregate;
using SouQna.Domain.Aggregates.UserAggregate;
using SouQna.Domain.Aggregates.ProductAggregate;
using SouQna.Domain.Aggregates.CategoryAggregate;

namespace SouQna.Application.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Category> Categories { get; }
        IRepository<Product> Products { get; }
        IRepository<Cart> Carts { get; }
        Task SaveChangesAsync();
    }
}