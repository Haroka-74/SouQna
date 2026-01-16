using MediatR;
using Microsoft.AspNetCore.Mvc;
using SouQna.Application.Common;
using SouQna.Application.DTOs.Categories;
using SouQna.Application.Features.Categories.Queries.GetCategory;
using SouQna.Application.Features.Categories.Commands.AddCategory;
using SouQna.Application.Features.Categories.Queries.GetCategories;
using SouQna.Application.Features.Categories.Commands.DeleteCategory;
using SouQna.Application.Features.Categories.Commands.UpdateCategory;

namespace SouQna.Presentation.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController(ISender sender) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<CategoryDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? orderBy = null,
            [FromQuery] bool isDescending = false
        )
            => Ok(await sender.Send(new GetCategoriesQuery(pageNumber, pageSize, orderBy, isDescending)));

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
            => Ok(await sender.Send(new GetCategoryQuery(id)));

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

        [HttpPut]
        [ProducesResponseType(typeof(UpdateCategoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(UpdateCategoryCommand command)
            => Ok(await sender.Send(command));

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