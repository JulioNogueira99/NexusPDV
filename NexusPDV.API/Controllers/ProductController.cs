using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusPDV.Application.UseCases.Product.Add;
using NexusPDV.Application.UseCases.Product.GetAll;
using NexusPDV.Application.UseCases.Product.GetById;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllProductsQuery());
            return Ok(result);
        }
    }
}
