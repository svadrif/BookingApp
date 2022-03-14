using Application.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Extentions;

public static class QueryableExtentions
{
    public static async Task<PagedList<TEntity>> ToPagedListAsync<TEntity>(this IQueryable<TEntity> source, PagedQueryBase query)
        where TEntity : class
    {
        var count = await source.CountAsync();
        var items = await source.Skip(query.SkipCount()).Take(query.PageSize).ToListAsync();

        return new PagedList<TEntity>(items, count, query.CurrentPage, query.PageSize);
    }

    public static IOrderedQueryable<TEntity> Sort<TEntity>(this IQueryable<TEntity> source, string sortBy, string sortDirection)
        where TEntity : class
    {
        return sortDirection.ToUpper() == "ASC" ? source.OrderBy(ToLambda<TEntity>(sortBy))
                                                : source.OrderByDescending(ToLambda<TEntity>(sortBy));
    }

    private static Expression<Func<T, object>> ToLambda<T>(string propertyname)
    {
        var parameter = Expression.Parameter(typeof(T));
        var property = Expression.Property(parameter, propertyname);
        var propAsObject = Expression.Convert(property, typeof(object));

        return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
    }
}
