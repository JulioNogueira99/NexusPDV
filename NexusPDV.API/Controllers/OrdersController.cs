using Microsoft.AspNetCore.Mvc;
using NexusPDV.Application.InputModels;
using NexusPDV.Application.Services;
using System;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using NexusPDV.Application.UseCases.Orders.PlaceOrder;
using NexusPDV.Application.UseCases.Orders.GetById;

namespace NexusPDV.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;
        private readonly IValidator<PlaceOrderInputModel> _validator;
        private readonly IMediator _mediator;

        public OrdersController(IOrderService service, IValidator<PlaceOrderInputModel> validator, IMediator mediator)
        {
            _service = service;
            _validator = validator;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PlaceOrderCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);

                return CreatedAtAction(nameof(GetById), new { id = response.OrderId }, response);
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Select(e => new { Field = e.PropertyName, Error = e.ErrorMessage });
                return BadRequest(new { message = "Erro de validação", errors });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediator.Send(new GetOrderByIdQuery(id));

            if (response == null)
            {
                return NotFound(new { message = "Pedido não encontrado." });
            }

            return Ok(response);
        }
    }
}