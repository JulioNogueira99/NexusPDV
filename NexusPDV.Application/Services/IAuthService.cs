using NexusPDV.Application.InputModels;
using NexusPDV.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusPDV.Application.Services
{
    public interface IAuthService
    {
        Task<LoginViewModel> Login(LoginInputModel model);
        Task<bool> Register(RegisterUserInputModel model);
    }
}
