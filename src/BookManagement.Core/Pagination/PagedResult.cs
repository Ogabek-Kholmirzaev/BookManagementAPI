namespace BookManagement.Core.Pagination;

public class PagedResult<T>(IEnumerable<T> items, int totalCount, int pageIndex, int pageSize)
    where T : class
{
    public IEnumerable<T> Items { get; } = items;
    public int TotalCount { get; } = totalCount;
    public int PageIndex { get; } = pageIndex;
    public int PageSize { get; } = pageSize;
}
