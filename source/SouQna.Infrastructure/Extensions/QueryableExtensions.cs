using System.Reflection;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace SouQna.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplySort<T>(
            this IQueryable<T> query,
            string? orderBy,
            bool isDescending = false
        ) where T : class
        {
            if(orderBy is null)
                return query.OrderBy(t => EF.Property<object>(t, "CreatedAt"));

            var property = typeof(T).GetProperty(
                orderBy,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance
            );

            if(property is null)
                return query.OrderBy(t => EF.Property<object>(t, "CreatedAt"));

            var parameter = Expression.Parameter(typeof(T), "t");
            var propertyAccess = Expression.Property(parameter, property);
            var lambda = Expression.Lambda(propertyAccess, parameter);

            var methodName = isDescending ? "OrderByDescending" : "OrderBy";
            var method = typeof(Queryable)
                .GetMethods()
                .First(m => m.Name == methodName && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), property.PropertyType);

            return (IQueryable<T>)method.Invoke(null, [query, lambda])!;
        }
    }
}