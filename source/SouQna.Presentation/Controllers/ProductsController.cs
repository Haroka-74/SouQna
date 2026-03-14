using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SouQna.Application.Features.Products.GetProduct;
using SouQna.Application.Features.Products.GetProducts;

namespace SouQna.Presentation.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController(ISender sender) : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductsAsync([FromQuery] GetProductsRequest request)
            => Ok(await sender.Send(request));

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductAsync(Guid id)
            => Ok(await sender.Send(new GetProductRequest(id)));
    }
}