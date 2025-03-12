using System;
using api.Models;
using api.Models.Enums;

namespace api.model;

public class Usuario
{
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; } // Login
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
        public ECredencial Credencial { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public int? CentralCustoId { get; set; } // Pode ser nulo caso um usuário não tenha uma central de custo atribuída
        public CentralCusto CentralCusto { get; set; }

}
