using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;
using api.model;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CentralCustoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CentralCustoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/centralcusto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CentralCusto>>> GetCentralCustos()
        {
            return await _context.CentralCustos.ToListAsync();
        }

        // GET: api/centralcusto/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CentralCusto>> GetCentralCusto(int id)
        {
            var centralCusto = await _context.CentralCustos.FindAsync(id);

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

        private bool CentralCustoExists(int id)
        {
            return _context.CentralCustos.Any(e => e.Id == id);
        }
    }
}
