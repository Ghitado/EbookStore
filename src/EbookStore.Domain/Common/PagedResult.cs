namespace EbookStore.Domain.Common;

public sealed record PagedResult<T>(
	IReadOnlyList<T> Items,
	int Page,
	int PageSize,
	int TotalCount)
{
	public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
	public bool HasNextPage => Page < TotalPages;
	public bool HasPreviousPage => Page > 1;

	public static PagedResult<T> Create(IEnumerable<T> items, int page, int pageSize, int totalCount)
		=> new(items.ToList().AsReadOnly(), page, pageSize, totalCount);
}