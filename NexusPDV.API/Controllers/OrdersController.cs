using Microsoft.AspNetCore.Mvc;
using NexusPDV.Application.InputModels;
using NexusPDV.Application.Services;
using System;
using System.Threading.Tasks;

namespace NexusPDV.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post(PlaceOrderInputModel input)
        {
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
    }
}