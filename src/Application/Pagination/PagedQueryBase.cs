namespace Application.Pagination;

public class PagedQueryBase
{
    public int PageSize { get; set; } = 10;
    public int CurrentPage { get; set; } = 1;
    public string SortOn { get; set; } = "Id";
    public string SortDirection { get; set; } = "ASC";

    public int SkipCount() => (CurrentPage - 1) * PageSize;
}