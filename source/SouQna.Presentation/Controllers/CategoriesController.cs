using MediatR;
using Microsoft.AspNetCore.Mvc;
using SouQna.Application.Features.Categories.Commands.AddCategory;
using SouQna.Application.Features.Categories.Commands.DeleteCategory;
using SouQna.Application.Features.Categories.Queries.GetCategoryById;

namespace SouQna.Presentation.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController(ISender sender) : ControllerBase
    {
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(GetCategoryByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
            => Ok(await sender.Send(new GetCategoryByIdQuery(id)));

        [HttpPost]
        [ProducesResponseType(typeof(AddCategoryResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateAsync(AddCategoryCommand command)
        {
            var result = await sender.Send(command);
            return CreatedAtAction("GetById", new { id = result.Id }, result);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await sender.Send(new DeleteCategoryCommand(id));
            return NoContent();
        }
    }
}