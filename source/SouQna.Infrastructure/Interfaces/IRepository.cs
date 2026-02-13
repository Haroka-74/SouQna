using System.Linq.Expressions;

namespace SouQna.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
    }
}