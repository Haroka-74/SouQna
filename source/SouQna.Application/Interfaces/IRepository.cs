using System.Linq.Expressions;

namespace SouQna.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<(IReadOnlyCollection<T> Items, int TotalCount)> GetPagedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null
        );
        Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T?> FindAsync(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes
        );
        Task<T?> FindAsync(
            Expression<Func<T, bool>> predicate,
            params string[] includePaths
        );
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
    }
}