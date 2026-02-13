using Microsoft.AspNetCore.Http;

namespace SouQna.Business.Contracts.Requests
{
    public record AddProductRequest(
        string Name,
        string Description,
        IFormFile Image,
        decimal Price
    );
}