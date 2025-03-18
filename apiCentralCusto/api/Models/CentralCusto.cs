using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using api.Models;

namespace api.Models
{
    public class CentralCusto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Descricao { get; set; }

        // Relacionamento 1:1 com Usuario
        [Required]
        public int? UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        [JsonIgnore]
        public Usuario? Usuario { get; set; }

        // Listas de lançamentos de entradas e saídas
        public List<LancamentoEntrada>? Entradas { get; set; } = new List<LancamentoEntrada>();
        public List<LancamentoSaida>? Saidas { get; set; } = new List<LancamentoSaida>();

        // Data de criação automática
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    }
}
