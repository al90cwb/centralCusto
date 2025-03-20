using api.Models.Enums;

namespace api.Models;

public class LancamentoSaida
{
      public int Id { get; set; }
        public CategoriaSaida CategoriaSaida { get; set; } // Relacionamento com CategoriaSaida
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
        public bool PagamentoConfirmado { get; set; }
        public EEstado EstadoLancamento { get; set; } // Usando enum EEstado
   
}
