namespace Application.Pagination;

public class PagedList<TEntity> : List<TEntity> where TEntity : class
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public PagedList(List<TEntity> items, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        CurrentPage = pageNumber;
        PageSize = pageSize;

        AddRange(items);
    }
}
