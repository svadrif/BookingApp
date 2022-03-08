using Application.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Application.Extentions;

public static class QueryableExtentions
{
    public static async Task<PagedList<TEntity>> ToPagedListAsync<TEntity>(this IQueryable<TEntity> source, PagedQueryBase query)
        where TEntity : class
    {
        var count = source.Count();
        var items = await source.Skip(query.SkipCount()).Take(query.PageSize).ToListAsync();

        return new PagedList<TEntity>(items, count, query.CurrentPage, query.PageSize);
    }
}
