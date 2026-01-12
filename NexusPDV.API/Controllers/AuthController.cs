using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexusPDV.Application.UseCases.Auth.Login;
using NexusPDV.Application.UseCases.Auth.Register;

namespace NexusPDV.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok(new { message = "Usuário criado com sucesso!" });
            }

            return BadRequest(new { message = "Erro ao criar usuário. Verifique se o email já existe." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            var loginResult = await _mediator.Send(command);

            if (loginResult == null)
            {
                return Unauthorized(new { message = "Email ou senha inválidos." });
            }

            // Retorna o Token para o usuário guardar no Front-end
            return Ok(loginResult);
        }
    }
}
