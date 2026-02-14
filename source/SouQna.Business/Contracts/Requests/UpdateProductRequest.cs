using Microsoft.AspNetCore.Http;

namespace SouQna.Business.Contracts.Requests
{
    public record UpdateProductRequest(
        string Name,
        string Description,
        decimal Price,
        IFormFile? Image = null
    );
}