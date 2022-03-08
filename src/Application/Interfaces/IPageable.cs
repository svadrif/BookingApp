using Application.Pagination;

namespace Application.Interfaces;

public interface IPageable<TEntity> where TEntity : class
{
    Task<PagedList<TEntity>> GetPagedAsync(PagedQueryBase query, bool tracking);
}
