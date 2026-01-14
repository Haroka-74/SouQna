using MediatR;
using Microsoft.AspNetCore.Mvc;
using SouQna.Application.Features.Categories.Commands.AddCategory;

namespace SouQna.Presentation.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController(ISender sender) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(AddCategoryResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateAsync(AddCategoryCommand command)
        {
            var result = await sender.Send(command);
            // return CreatedAtAction("GetCategoryById", new { id = result.Id }, result);
            return Ok(result);
        }
    }
}