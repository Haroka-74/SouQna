using MediatR;
using Microsoft.AspNetCore.Mvc;
using SouQna.Presentation.Contracts.Products;
using SouQna.Application.Features.Products.Commands.AddProduct;

namespace SouQna.Presentation.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController(ISender sender) : ControllerBase
    {
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