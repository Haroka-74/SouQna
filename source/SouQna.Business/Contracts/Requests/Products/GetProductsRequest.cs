namespace SouQna.Business.Contracts.Requests.Products
{
    public record GetProductsRequest(
        int PageNumber = 1,
        int PageSize = 10,
        string? SearchTerm = null,
        decimal? MinPrice = null,
        decimal? MaxPrice = null,
        string? SortColumn = null,
        string? SortOrder = null
    );
}