using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;
using api.Services;
using api.Data;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CentralCustoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly CentralCustoService _service;

        public CentralCustoController(AppDbContext context, CentralCustoService service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/centralcusto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CentralCusto>>> GetCentralCustos()
        {
            return await _context.CentralCustos
                .Include(c => c.Entradas)
                .Include(c => c.Saidas)
                .ToListAsync();
        }

        // GET: api/centralcusto/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CentralCusto>> GetCentralCusto(int id)
        {
            var centralCusto = await _context.CentralCustos
                .Include(c => c.Entradas)
                .Include(c => c.Saidas)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (centralCusto == null)
            {
                return NotFound();
            }

            return centralCusto;
        }

        // POST: api/centralcusto
        [HttpPost]
        public async Task<ActionResult<CentralCusto>> PostCentralCusto(CentralCusto centralCusto)
        {
            _context.CentralCustos.Add(centralCusto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCentralCusto), new { id = centralCusto.Id }, centralCusto);
        }

        // PUT: api/centralcusto/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCentralCusto(int id, CentralCusto centralCusto)
        {
            if (id != centralCusto.Id)
            {
                return BadRequest();
            }

            _context.Entry(centralCusto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CentralCustoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/centralcusto/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCentralCusto(int id)
        {
            var centralCusto = await _context.CentralCustos.FindAsync(id);
            if (centralCusto == null)
            {
                return NotFound();
            }

            _context.CentralCustos.Remove(centralCusto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Editar lançamento de entrada
        [HttpPut("{id}/entrada/{entradaId}")]
        public async Task<IActionResult> EditarLancamentoEntrada(int id, int entradaId, LancamentoEntrada lancamentoEntrada)
        {
            var centralCusto = await _context.CentralCustos.Include(c => c.Entradas).FirstOrDefaultAsync(c => c.Id == id);
            if (centralCusto == null)
            {
                return NotFound("Central de Custo não encontrada.");
            }

            var entrada = centralCusto.Entradas.FirstOrDefault(e => e.Id == entradaId);
            if (entrada == null)
            {
                return NotFound("Lançamento de Entrada não encontrado.");
            }

            entrada.Descricao = lancamentoEntrada.Descricao;
            entrada.Valor = lancamentoEntrada.Valor;
            entrada.DataVencimento = lancamentoEntrada.DataVencimento;
            entrada.DataInicio = lancamentoEntrada.DataInicio;
            entrada.DuracaoMeses = lancamentoEntrada.DuracaoMeses;
            entrada.PagamentoConfirmado = lancamentoEntrada.PagamentoConfirmado;
            entrada.EstadoLancamento = lancamentoEntrada.EstadoLancamento;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Editar lançamento de saída
        [HttpPut("{id}/saida/{saidaId}")]
        public async Task<IActionResult> EditarLancamentoSaida(int id, int saidaId, LancamentoSaida lancamentoSaida)
        {
            var centralCusto = await _context.CentralCustos.Include(c => c.Saidas).FirstOrDefaultAsync(c => c.Id == id);
            if (centralCusto == null)
            {
                return NotFound("Central de Custo não encontrada.");
            }

            var saida = centralCusto.Saidas.FirstOrDefault(s => s.Id == saidaId);
            if (saida == null)
            {
                return NotFound("Lançamento de Saída não encontrado.");
            }

            saida.Descricao = lancamentoSaida.Descricao;
            saida.Valor = lancamentoSaida.Valor;
            saida.DataVencimento = lancamentoSaida.DataVencimento;
            saida.DataPagamento = lancamentoSaida.DataPagamento;
            saida.PagamentoConfirmado = lancamentoSaida.PagamentoConfirmado;
            saida.EstadoLancamento = lancamentoSaida.EstadoLancamento;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Obter visão mensal
        [HttpGet("{id}/visao-mensal")]
        public async Task<ActionResult<VisaoMensalDTO>> GetVisaoMensal(int id, [FromQuery] int ano, [FromQuery] int mes)
        {
            var visaoMensal = await _service.ObterVisaoMensal(id, ano, mes);
            if (visaoMensal == null)
            {
                return NotFound("Central de Custo não encontrada.");
            }

            return Ok(visaoMensal);
        }

        private bool CentralCustoExists(int id)
        {
            return _context.CentralCustos.Any(e => e.Id == id);
        }
    }
}
