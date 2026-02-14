using SouQna.Infrastructure.Entities;

namespace SouQna.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Product> Products { get; }
        IRepository<Cart> Carts { get; }
        IRepository<CartItem> CartItems { get; }
        Task SaveChangesAsync();
    }
}