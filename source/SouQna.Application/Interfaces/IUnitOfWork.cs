using SouQna.Domain.Entities;

namespace SouQna.Application.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<User> Users { get; }
        IRepository<UserRole> UserRoles { get; }
        IRepository<Role> Roles { get; }
        IRepository<Product> Products { get; }
        IRepository<Cart> Carts { get; }
        IRepository<CartItem> CartItems { get; }
        IRepository<Inventory> Inventories { get; }
        IRepository<Order> Orders { get; }
        IRepository<OrderItem> OrderItems { get; }
        IRepository<Payment> Payments { get; }
        Task SaveChangesAsync();
    }
}