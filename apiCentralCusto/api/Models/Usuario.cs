using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Models.Enums;

namespace api.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } // Login

        [Required]
        public string Senha { get; set; } // Senha criptografada depois

        public DateTime DataNascimento { get; set; }

        [Required]
        public ECredencial Credencial { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        // Relacionamento 1:1 com CentralCusto
        public CentralCusto CentralCusto { get; set; }

        [ForeignKey("CentralCusto")]
        public int? CentralCustoId { get; set; } // Pode ser nulo caso um usuário não tenha uma central de custo atribuída
    }
}
