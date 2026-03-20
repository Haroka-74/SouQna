using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SouQna.Application.Features.Products.GetProduct;
using SouQna.Application.Features.Products.GetProducts;
using SouQna.Application.Features.Products.CreateProduct;
using SouQna.Application.Features.Products.UpdateProduct;
using SouQna.Application.Features.Products.DeleteProduct;
using SouQna.Application.Features.Products.GetAdminProducts;

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

        [HttpGet("admin")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAdminProductsAsync([FromQuery] GetAdminProductsRequest request)
        {
            return Ok(await sender.Send(request));
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateProductAsync([FromForm] Contracts.CreateProductRequest request)
        {
            return StatusCode(
                StatusCodes.Status201Created,
                await sender.Send(
                    new CreateProductRequest(
                        request.Name,
                        request.Description,
                        request.Image.OpenReadStream(),
                        request.Price,
                        request.Quantity
                    )
                )
            );
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateProductAsync(Guid id, [FromForm] Contracts.UpdateProductRequest request)
        {
            return Ok(
                await sender.Send(
                    new UpdateProductRequest(
                        id,
                        request.Name,
                        request.Description,
                        request.Image?.OpenReadStream(),
                        request.Price
                    )
                )
            );
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteProductAsync(Guid id)
        {
            await sender.Send(new DeleteProductRequest(id));
            return NoContent();
        }
    }
}