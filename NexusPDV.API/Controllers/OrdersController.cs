using Microsoft.AspNetCore.Mvc;
using NexusPDV.Application.InputModels;
using NexusPDV.Application.Services;
using System;
using System.Threading.Tasks;
using FluentValidation;

namespace NexusPDV.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;
        private readonly IValidator<PlaceOrderInputModel> _validator;

        public OrdersController(IOrderService service, IValidator<PlaceOrderInputModel> validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(PlaceOrderInputModel input)
        {
            var validationResult = await _validator.ValidateAsync(input);
            if (!validationResult.IsValid)
            {
                return BadRequest(new { errors = validationResult.Errors });
            }

            try
            { 
                var order = await _service.PlaceOrder(input);

                return CreatedAtAction(nameof(Post), new { id = order.OrderId }, order);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocorreu um erro interno: " + ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _service.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }
    }
}