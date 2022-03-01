namespace Application.Pagination;

public class PagedResultBase : PagedBase
{
    public int RowCount { get; set; }
    
    public int PageCount => (int)Math.Ceiling((double)RowCount / PageSize);
    public int FirstRowOnPage => (CurrentPage - 1) * PageSize + 1;
    public int LastRowOnPage => Math.Min(CurrentPage * PageSize, RowCount);
}
