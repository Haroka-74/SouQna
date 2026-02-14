using Microsoft.AspNetCore.Mvc;
using SouQna.Business.Interfaces;
using SouQna.Business.Contracts.Requests;
using Microsoft.AspNetCore.Authorization;

namespace SouQna.Presentation.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductsAsync([FromQuery] GetProductsRequest request)
            => Ok(await productService.GetPagedProductsAsync(request));

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductAsync(Guid id)
            => Ok(await productService.GetProductAsync(id));

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddProductAsync([FromForm] AddProductRequest request)
            => CreatedAtAction("AddProduct", await productService.AddProductAsync(request));

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}