using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SouQna.Application.Features.Inventories.Admin.AdjustStock;

namespace SouQna.Presentation.Controllers.Inventories
{
    [Route("api/admin/inventories")]
    [ApiController]
    [Authorize(Roles = "admin")]
    [Tags("Admin - Inventories")]
    public class AdminController(ISender sender) : ControllerBase
    {
        [HttpPut("adjust/{productId:guid}")]
        public async Task<IActionResult> AdjustStock(Guid productId, Contracts.Inventories.AdjustStockRequest request)
        {
            await sender.Send(new AdjustStockRequest(productId, request.Quantity));
            return NoContent();
        }
    }
}