using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusPDV.Application.UseCases.Product.Add;

namespace NexusPDV.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddProductCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(Post), new { id = result.ProductId }, result);
        }
    }
}
