using Microsoft.AspNetCore.Mvc;
using NexusPDV.Application.InputModels;
using NexusPDV.Application.Services;

namespace NexusPDV.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserInputModel model)
        {
            var result = await _authService.Register(model);

            if (result)
            {
                return Ok(new { message = "Usuário criado com sucesso!" });
            }

            return BadRequest(new { message = "Erro ao criar usuário. Verifique se o email já existe." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            var loginResult = await _authService.Login(model);

            if (loginResult == null)
            {
                return Unauthorized(new { message = "Email ou senha inválidos." });
            }

            // Retorna o Token para o usuário guardar no Front-end
            return Ok(loginResult);
        }
    }
}
