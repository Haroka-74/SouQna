using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SouQna.Infrastructure.Interfaces;

namespace SouQna.Infrastructure.Persistence.Repositories
{
    public class Repository<T>(SouQnaDbContext context) : IRepository<T> where T : class
    {
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
            => await context.Set<T>().AnyAsync(predicate);

        public async Task<T> AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            return entity;
        }
    }
}