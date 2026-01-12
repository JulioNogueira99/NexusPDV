using MediatR;
using Microsoft.AspNetCore.Identity;
using NexusPDV.Application.UseCases.Auth.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusPDV.Application.UseCases.Auth.Login
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JwtTokenGenerator _tokenGenerator;

        public LoginUserHandler(SignInManager<IdentityUser> signInManager, JwtTokenGenerator tokenGenerator)
        {
            _signInManager = signInManager;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<LoginUserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, true);
            if (result.Succeeded)
            {
                return _tokenGenerator.Generate(request.Email);
            }

            return null;
        }


    }
}
