using System.Linq.Expressions;

namespace SouQna.Infrastructure.Interfaces
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
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
    }
}