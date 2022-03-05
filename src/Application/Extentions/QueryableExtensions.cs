using Application.Pagination;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Extentions;

public static class QueryableExtentions
{
    public static async Task<TResponse> GetPagedAsync<TResponse, TEntity, TDto>(this IQueryable<TEntity> source,
        PagedQueryBase query, IMapper mapper, Expression<Func<TEntity, object>> customSortBy = null,
        Expression<Func<TEntity, object>> thenSortBy = null)
        where TEntity : class 
        where TDto : class
        where TResponse : PagedResult<TDto>, new()
    {
        if(!string.IsNullOrEmpty(query.SortDirection))
        {
            if(customSortBy != null)
            {
                if(query.SortDirection.ToUpper() == "ASC")
                {
                    source = thenSortBy != null 
                        ? source.OrderBy(customSortBy).ThenBy(thenSortBy)
                        : source.OrderBy(customSortBy);
                }
                else
                {
                    source = thenSortBy != null
                       ? source.OrderByDescending(customSortBy).ThenByDescending(thenSortBy)
                       : source.OrderByDescending(customSortBy);
                }
            }
            else if(!string.IsNullOrEmpty(query.SortOn))
            {
                source = query.SortDirection.ToUpper() == "ASC"
                    ? source.OrderBy(query.SortOn)
                    : source.OrderByDescending(query.SortOn);
            }
        }

        return new TResponse
        {
            CurrentPage = query.CurrentPage,
            PageSize = query.PageSize,
            RowCount = await source.CountAsync(),
            Results = await source.Skip(query.SkipCount()).Take(query.PageSize).ProjectTo<TDto>(mapper.ConfigurationProvider).ToListAsync()
        };
    }

    public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
    {
        return source.OrderBy(ToLambda<T>(propertyName));
    }

    public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
    {
        return source.OrderBy(ToLambda<T>(propertyName));
    }

    private static Expression<Func<T, object>> ToLambda<T>(string propertyname)
    {
        var parameter = Expression.Parameter(typeof(T));
        var property = Expression.Property(parameter, propertyname);
        var propAsObject = Expression.Convert(property, typeof(object));

        return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
    }
}
