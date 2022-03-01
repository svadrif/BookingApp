namespace Application.Pagination;

public abstract class PagedBase
{
    public int PageSize { get; set; } = 10;
    public int CurrentPage { get; set; } = 1;

    public int SkipCount => (CurrentPage - 1) * PageSize;
}
