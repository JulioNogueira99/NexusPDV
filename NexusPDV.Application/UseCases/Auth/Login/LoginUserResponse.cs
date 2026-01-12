using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusPDV.Application.UseCases.Auth.Login
{
    public class LoginUserResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
