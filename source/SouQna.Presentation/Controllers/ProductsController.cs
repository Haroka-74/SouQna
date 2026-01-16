using MediatR;
using Microsoft.AspNetCore.Mvc;
using SouQna.Application.DTOs.Products;
using SouQna.Presentation.Contracts.Products;
using SouQna.Application.Features.Products.Commands.AddProduct;
using SouQna.Application.Features.Products.Queries.GetProduct;

namespace SouQna.Presentation.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController(ISender sender) : ControllerBase
    {
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
    }
}