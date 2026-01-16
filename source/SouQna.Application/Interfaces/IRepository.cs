using System.Linq.Expressions;

namespace SouQna.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<(IReadOnlyCollection<T> Items, int TotalCount)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            string? orderBy = null,
            bool isDescending = false,
            params Expression<Func<T, object>>[] includes
        );
        Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T?> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task DeleteAsync(T entity);
    }
}