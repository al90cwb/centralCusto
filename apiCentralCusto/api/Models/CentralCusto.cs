using System;
using api.model;

namespace api.Models;

public class CentralCusto
{
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int UsuarioId { get; set; } // Relacionamento com Usuario
        public List<LancamentoEntrada> Entradas { get; set; } = new List<LancamentoEntrada>();
        public List<LancamentoSaida> Saidas { get; set; } = new List<LancamentoSaida>();
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public Usuario Usuario { get; set; }
}
