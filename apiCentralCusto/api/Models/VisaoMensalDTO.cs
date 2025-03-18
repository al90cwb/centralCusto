using System;

namespace api.Models;

  public class VisaoMensalDTO
    {
        public int Ano { get; set; }
        public int Mes { get; set; }
        
        public List<LancamentoEntrada> Entradas { get; set; } = new();
        public List<LancamentoSaida> Saidas { get; set; } = new();

        public double SomaEntradas { get; set; }
        public double SomaSaidas { get; set; }
        public double SaldoMes { get; set; }
        public double SaldoMesAcumulado { get; set; }

        public double SomaEntradasConfirmadas { get; set; }
        public double SomaSaidasConfirmadas { get; set; }
        public double SaldoMesConfirmado { get; set; }
        public double SaldoMesAcumuladoConfirmado { get; set; }
    }