using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NexusPDV.Application.InputModels;
using NexusPDV.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NexusPDV.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<bool> Register(RegisterUserInputModel model)
        {
            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true // Para simplificar, já confirmamos o email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            return result.Succeeded;
        }

        public async Task<LoginViewModel> Login(LoginInputModel model)
        {
            // 1. Verificar se o utilizador/senha batem
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);

            if (result.Succeeded)
            {
                // 2. Se deu certo, gerar o Token
                return GenerateJwtToken(model.Email);
            }

            return null; // Login falhou
        }

        private LoginViewModel GenerateJwtToken(string email)
        {
            // Busca as configurações do appsettings.json
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

            // Define as Claims (dados que vão dentro do token, como o email)
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Cria a chave de assinatura
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2), // Token vale por 2 horas
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new LoginViewModel
            {
                Token = tokenHandler.WriteToken(token),
                Expiration = tokenDescriptor.Expires.Value
            };
        }
    }
}
