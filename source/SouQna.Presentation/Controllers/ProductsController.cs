using Microsoft.AspNetCore.Mvc;
using SouQna.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using SouQna.Business.Contracts.Requests.Products;

namespace SouQna.Presentation.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductsAsync([FromQuery] GetProductsRequest request)
            => Ok(await productService.GetProductsAsync(request));

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductAsync(Guid id)
            => Ok(await productService.GetProductAsync(id));

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddProductAsync([FromForm] AddProductRequest request)
        {
            var result = await productService.AddProductAsync(request);
            return CreatedAtAction("GetProduct", new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateProductAsync(Guid id, [FromForm] UpdateProductRequest request)
        {
            await productService.UpdateProductAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteProductAsync(Guid id)
        {
            await productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}