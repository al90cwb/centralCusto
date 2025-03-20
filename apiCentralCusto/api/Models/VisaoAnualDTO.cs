using System.Collections.Generic;

namespace api.Models;
    public class VisaoAnualDTO
    {
        public int Ano { get; set; }
        public decimal TotalEntradasAno { get; set; }
        public decimal TotalSaidasAno { get; set; }
        public Dictionary<int, decimal> TotalEntradasPorMes { get; set; }
        public Dictionary<int, decimal> TotalSaidasPorMes { get; set; }
        public Dictionary<int, Dictionary<string, decimal>> TotalEntradasPorCategoriaPorMes { get; set; }
        public Dictionary<int, Dictionary<string, decimal>> TotalSaidasPorCategoriaPorMes { get; set; }

        public VisaoAnualDTO()
        {
            TotalEntradasPorMes = new Dictionary<int, decimal>();
            TotalSaidasPorMes = new Dictionary<int, decimal>();
            TotalEntradasPorCategoriaPorMes = new Dictionary<int, Dictionary<string, decimal>>();
            TotalSaidasPorCategoriaPorMes = new Dictionary<int, Dictionary<string, decimal>>();
        }
    }

