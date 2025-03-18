
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class CentralCustoService
    {
        private readonly AppDbContext _context;

        public CentralCustoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<VisaoMensalDTO> ObterVisaoMensal(int centralCustoId, int ano, int mes)
        {
            var centralCusto = await _context.CentralCustos
                .Include(c => c.Entradas)
                .Include(c => c.Saidas)
                .FirstOrDefaultAsync(c => c.Id == centralCustoId);

            if (centralCusto == null)
            {
                return null;
            }

            var entradasMes = centralCusto.Entradas
                .Where(e => e.DataVencimento.Year == ano && e.DataVencimento.Month == mes)
                .ToList();

            var saidasMes = centralCusto.Saidas
                .Where(s => s.DataVencimento.Year == ano && s.DataVencimento.Month == mes)
                .ToList();

            var somaEntradas = entradasMes.Sum(e => e.Valor);
            var somaSaidas = saidasMes.Sum(s => s.Valor);
            var saldoMes = somaEntradas - somaSaidas;

            var somaEntradasConfirmadas = entradasMes.Where(e => e.PagamentoConfirmado).Sum(e => e.Valor);
            var somaSaidasConfirmadas = saidasMes.Where(s => s.PagamentoConfirmado).Sum(s => s.Valor);
            var saldoMesConfirmado = somaEntradasConfirmadas - somaSaidasConfirmadas;

            // Calculando saldo acumulado
            var saldoAcumulado = await CalcularSaldoAcumulado(centralCustoId, ano, mes);
            var saldoAcumuladoConfirmado = await CalcularSaldoAcumuladoConfirmado(centralCustoId, ano, mes);

            return new VisaoMensalDTO
            {
                Ano = ano,
                Mes = mes,
                Entradas = entradasMes,
                Saidas = saidasMes,
                SomaEntradas = somaEntradas,
                SomaSaidas = somaSaidas,
                SaldoMes = saldoMes,
                SaldoMesAcumulado = saldoMes + saldoAcumulado,
                SomaEntradasConfirmadas = somaEntradasConfirmadas,
                SomaSaidasConfirmadas = somaSaidasConfirmadas,
                SaldoMesConfirmado = saldoMesConfirmado,
                SaldoMesAcumuladoConfirmado = saldoMesConfirmado + saldoAcumuladoConfirmado
            };
        }

        private async Task<double> CalcularSaldoAcumulado(int centralCustoId, int ano, int mes)
        {
            var entradas = await _context.CentralCustos
                .Where(c => c.Id == centralCustoId)
                .SelectMany(c => c.Entradas)
                .Where(e => e.DataVencimento.Year < ano || (e.DataVencimento.Year == ano && e.DataVencimento.Month < mes))
                .SumAsync(e => e.Valor);

            var saidas = await _context.CentralCustos
                .Where(c => c.Id == centralCustoId)
                .SelectMany(c => c.Saidas)
                .Where(s => s.DataVencimento.Year < ano || (s.DataVencimento.Year == ano && s.DataVencimento.Month < mes))
                .SumAsync(s => s.Valor);

            return entradas - saidas;
        }

        private async Task<double> CalcularSaldoAcumuladoConfirmado(int centralCustoId, int ano, int mes)
        {
            var entradasConfirmadas = await _context.CentralCustos
                .Where(c => c.Id == centralCustoId)
                .SelectMany(c => c.Entradas)
                .Where(e => e.PagamentoConfirmado && (e.DataVencimento.Year < ano || (e.DataVencimento.Year == ano && e.DataVencimento.Month < mes)))
                .SumAsync(e => e.Valor);

            var saidasConfirmadas = await _context.CentralCustos
                .Where(c => c.Id == centralCustoId)
                .SelectMany(c => c.Saidas)
                .Where(s => s.PagamentoConfirmado && (s.DataVencimento.Year < ano || (s.DataVencimento.Year == ano && s.DataVencimento.Month < mes)))
                .SumAsync(s => s.Valor);

            return entradasConfirmadas - saidasConfirmadas;
        }

        public async Task<VisaoAnualDTO> ObterVisaoAnual(int centralCustoId, int ano)
        {
            var centralCusto = await _context.CentralCustos
                .Include(c => c.Entradas)
                .Include(c => c.Saidas)
                .FirstOrDefaultAsync(c => c.Id == centralCustoId);

            if (centralCusto == null)
            {
                return null;
            }

            var visaoAnual = new VisaoAnualDTO { Ano = ano };

            double totalEntradasAno = 0;
            double totalSaidasAno = 0;

            for (int mes = 1; mes <= 12; mes++)
            {
                var entradasMes = centralCusto.Entradas
                    .Where(e => e.DataVencimento.Year == ano && e.DataVencimento.Month == mes)
                    .ToList();

                var saidasMes = centralCusto.Saidas
                    .Where(s => s.DataVencimento.Year == ano && s.DataVencimento.Month == mes)
                    .ToList();

                double totalEntradasMes = entradasMes.Sum(e => e.Valor);
                double totalSaidasMes = saidasMes.Sum(s => s.Valor);

                visaoAnual.TotalEntradasPorMes[mes] = (decimal)totalEntradasMes;
                visaoAnual.TotalSaidasPorMes[mes] = (decimal)totalSaidasMes;

                totalEntradasAno += totalEntradasMes;
                totalSaidasAno += totalSaidasMes;

                visaoAnual.TotalEntradasPorCategoriaPorMes[mes] = entradasMes
                    .GroupBy(e => e.CategoriaEntrada.Nome)
                    .ToDictionary(g => g.Key, g => (decimal)g.Sum(e => e.Valor));

                visaoAnual.TotalSaidasPorCategoriaPorMes[mes] = saidasMes
                    .GroupBy(s => s.CategoriaSaida.Nome)
                    .ToDictionary(g => g.Key, g => (decimal)g.Sum(s => s.Valor));
            }

            visaoAnual.TotalEntradasAno = (decimal)totalEntradasAno;
            visaoAnual.TotalSaidasAno = (decimal)totalSaidasAno;

            return visaoAnual;
        }
    }
}