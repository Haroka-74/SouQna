using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SouQna.Application.Features.Products.Customer.GetProduct;
using SouQna.Application.Features.Products.Customer.GetProducts;

namespace SouQna.Presentation.Controllers.Products
{
    [Route("api/customer/products")]
    [ApiController]
    [AllowAnonymous]
    [Tags("Customer - Products")]
    public class CustomerController(ISender sender) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] GetProductsRequest request)
            => Ok(await sender.Send(request));

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProduct(Guid id)
            => Ok(await sender.Send(new GetProductRequest(id)));
    }
}