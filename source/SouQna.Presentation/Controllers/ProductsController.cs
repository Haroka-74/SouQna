using MediatR;
using Microsoft.AspNetCore.Mvc;
using SouQna.Application.Common;
using SouQna.Application.DTOs.Products;
using SouQna.Presentation.Contracts.Products;
using SouQna.Application.Features.Products.Queries.GetProduct;
using SouQna.Application.Features.Products.Queries.GetProducts;
using SouQna.Application.Features.Products.Commands.AddProduct;
using SouQna.Application.Features.Products.Commands.DeleteProduct;
using SouQna.Application.Features.Products.Commands.UpdateProduct;

namespace SouQna.Presentation.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController(ISender sender) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<ProductDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] Guid? categoryId = null,
            [FromQuery] string? searchTerm = null,
            [FromQuery] bool? isActive = null,
            [FromQuery] string? orderBy = null,
            [FromQuery] bool isDescending = false
        )
            => Ok(
                await sender.Send(
                    new GetProductsQuery(
                        pageNumber,
                        pageSize,
                        categoryId,
                        searchTerm,
                        isActive,
                        orderBy,
                        isDescending
                    )
                )
            );

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
            => Ok(await sender.Send(new GetProductQuery(id)));

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(AddProductResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateAsync([FromForm] AddProductRequest request)
        {
            var result = await sender.Send(new AddProductCommand(
                request.Name,
                request.Description,
                request.Image.OpenReadStream(),
                request.Image.FileName,
                request.Price,
                request.Quantity,
                request.IsActive,
                request.CategoryId
            ));
            return CreatedAtAction("GetById", new { id = result.Id }, result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(UpdateProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(UpdateProductCommand command)
            => Ok(await sender.Send(command));

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await sender.Send(new DeleteProductCommand(id));
            return NoContent();
        }
    }
}