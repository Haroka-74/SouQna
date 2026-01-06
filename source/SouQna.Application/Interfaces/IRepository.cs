using System.Linq.Expressions;

namespace SouQna.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
    }
}