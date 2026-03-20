using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SouQna.Application.Features.Inventories.AdjustStock;

namespace SouQna.Presentation.Controllers
{
    [Route("api/inventories")]
    [ApiController]
    public class InventoriesController(ISender sender) : ControllerBase
    {
        [HttpPatch("adjust-stock")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AdjustStockAsync(AdjustStockRequest request)
        {
            await sender.Send(request);
            return NoContent();
        }
    }
}