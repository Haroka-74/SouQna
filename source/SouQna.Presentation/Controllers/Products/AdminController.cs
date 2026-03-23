using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SouQna.Application.Features.Products.Admin.GetProducts;
using SouQna.Application.Features.Products.Admin.CreateProduct;
using SouQna.Application.Features.Products.Admin.UpdateProduct;
using SouQna.Application.Features.Products.Admin.DeleteProduct;

namespace SouQna.Presentation.Controllers.Products
{
    [Route("api/admin/products")]
    [ApiController]
    [Authorize(Roles = "admin")]
    [Tags("Admin - Products")]
    public class AdminController(ISender sender) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] GetProductsRequest request)
            => Ok(await sender.Send(request));

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] Contracts.Products.CreateProductRequest request)
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
        public async Task<IActionResult> UpdateProduct(Guid id, [FromForm] Contracts.Products.UpdateProductRequest request)
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
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await sender.Send(new DeleteProductRequest(id));
            return NoContent();
        }
    }
}