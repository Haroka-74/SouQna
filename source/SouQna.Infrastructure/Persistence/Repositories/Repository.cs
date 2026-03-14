using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SouQna.Application.Interfaces;

namespace SouQna.Infrastructure.Persistence.Repositories
{
    public class Repository<T>(SouQnaDbContext context) : IRepository<T> where T : class
    {
        public async Task<(IReadOnlyCollection<T> Items, int TotalCount)> GetPagedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null
        )
        {
            IQueryable<T> query = context.Set<T>().AsNoTracking();

            if(filter is not null)
                query = query.Where(filter);

            int totalCount = await query.CountAsync();

            if(orderBy is not null)
                query = orderBy(query);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
            => await context.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
            => await context.Set<T>().AnyAsync(predicate);

        public async Task<T> AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            return entity;
        }
    }
}