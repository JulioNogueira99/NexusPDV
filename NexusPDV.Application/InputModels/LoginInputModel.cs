using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusPDV.Application.InputModels
{
    public class LoginInputModel
    {
        [Required(ErrorMessage = "O Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A Senha é obrigatória")]
        public string Password { get; set; }
    }
}
