using System;
using api.Models.Enums;

namespace api.Models;

public class LancamentoEntrada
{
        public int Id { get; set; }
        public CategoriaEntrada CategoriaEntrada { get; set; } // Relacionamento com CategoriaEntrada
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataInicio { get; set; }
        public int DuracaoMeses { get; set; }
        public bool PagamentoConfirmado { get; set; }
        public EEstado EstadoLancamento { get; set; } // Usando enum EEstado
  
}
