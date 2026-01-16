namespace SouQna.Application.Common
{
    public class PagedResult<T>(
        int pageNumber,
        int pageSize,
        int totalCount,
        IReadOnlyCollection<T> items
    )
    {
        public int TotalPages => (int) Math.Ceiling(totalCount / (double) pageSize);
        public bool HasNextPage => pageNumber < TotalPages;
        public bool HasPreviousPage => pageNumber > 1;
        public IReadOnlyCollection<T> Items => items;
    }
}