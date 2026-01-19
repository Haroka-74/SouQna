using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SouQna.Application.Interfaces;
using SouQna.Infrastructure.Extensions;

namespace SouQna.Infrastructure.Persistence.Repositories
{
    public class Repository<T>(SouQnaDbContext context) : IRepository<T> where T : class
    {
        public async Task<IReadOnlyCollection<T>> FindAllAsync(Expression<Func<T, bool>>? predicate)
        {
            IQueryable<T> query = context.Set<T>().AsNoTracking();

            if(predicate is not null)
                query = query.Where(predicate);

            return await query.ToListAsync();
        }

        public async Task<(IReadOnlyCollection<T> Items, int TotalCount)> GetPagedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            string? orderBy = null,
            bool isDescending = false,
            Expression<Func<T, bool>>? predicate = null,
            params Expression<Func<T, object>>[] includes
        )
        {
            IQueryable<T> query = context.Set<T>().AsNoTracking();

            if(predicate is not null)
                query = query.Where(predicate);

            int totalCount = await query.CountAsync();

            foreach(var include in includes)
                query = query.Include(include);

            query = query.ApplySort(orderBy, isDescending);

            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return (items, totalCount);
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
            => await context.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task<T?> FindAsync(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes
        )
        {
            IQueryable<T> query = context.Set<T>();

            foreach(var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
            => await context.Set<T>().AnyAsync(predicate);

        public async Task<T> AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            return entity;
        }

        public Task DeleteAsync(T entity)
        {
            context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }
    }
}