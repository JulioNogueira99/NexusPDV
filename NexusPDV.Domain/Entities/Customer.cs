using NexusPDV.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusPDV.Domain.Entities
{
    public class Customer : Entity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        // Construtor vazio para o EF Core
        protected Customer() { }

        public Customer(string name, string email, string cpf)
        {
            ValidateDomain(name, email, cpf);
            Name = name;
            Email = email;
            Cpf = cpf;
        }

        public void Update(string name, string email)
        {
            ValidateDomain(name, email, Cpf);
            Name = name;
            Email = email;
        }

        private void ValidateDomain(string name, string email, string cpf)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Nome é obrigatório");
            if (string.IsNullOrEmpty(email)) throw new ArgumentException("Email é obrigatório");
            if (string.IsNullOrEmpty(cpf)) throw new ArgumentException("CPF é obrigatório");
        }
    }
}
