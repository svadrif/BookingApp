namespace Application.Pagination;

public class PagedQueryBase : PagedBase
{
    public string SortOn { get; set; }
    public string SortDirection { get; set; }
}
