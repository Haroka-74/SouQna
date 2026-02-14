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
        public async Task<IActionResult> GetProducts([FromQuery] GetProductsRequest request)
            => Ok(await productService.GetPagedProductsAsync(request));

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddProductAsync([FromForm] AddProductRequest request)
            => CreatedAtAction("AddProduct", await productService.AddProductAsync(request));
    }
}