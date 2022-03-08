namespace Application.Pagination;

public class PagedQueryBase
{
    public int PageSize { get; set; } = 10;
    public int CurrentPage { get; set; } = 1;
    public string SortOn { get; set; }
    public string SortDirection { get; set; }

    public int SkipCount() => (CurrentPage - 1) * PageSize;
}