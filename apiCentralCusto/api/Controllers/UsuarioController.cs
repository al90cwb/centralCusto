using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.model;
using api.Models;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/usuario/cadastrar
        [HttpPost("cadastrar")]
        public async Task<ActionResult<Usuario>> CadastrarUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null)
                return BadRequest("Usuário inválido");

            // Verifica se já existe um usuário com o mesmo email
            var usuarioExistente = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == usuario.Email);

            if (usuarioExistente != null)
                return Conflict("Já existe um usuário com este email.");

            // Cria a central de custo associada ao usuário
            var centralCusto = new CentralCusto
            {
                Descricao = "Central de Custo de " + usuario.Nome,
                UsuarioId = usuario.Id,
                DataCriacao = DateTime.Now
            };

            // Adiciona a central de custo ao banco de dados
            _context.CentralCustos.Add(centralCusto);
            await _context.SaveChangesAsync();

            // Atribui a central de custo criada ao usuário
            usuario.CentralCustoId = centralCusto.Id;

            // Adiciona o usuário ao banco de dados
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CadastrarUsuario), new { id = usuario.Id }, usuario);
        }

        [HttpDelete("excluir/{id}")]
        public async Task<IActionResult> ExcluirUsuario(int id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.CentralCusto) // Incluir a central de custo relacionada
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            // Remove o usuário (a central de custo será excluída em cascata)
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent(); // Retorna 204 No Content
        }

        [HttpPut("atualizar/{id}")]
        public async Task<IActionResult> AtualizarUsuario(int id, [FromBody] Usuario usuarioAtualizado)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.CentralCusto) // Incluir a central de custo relacionada
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            // Atualiza os campos permitidos
            usuario.Nome = usuarioAtualizado.Nome;
            usuario.Email = usuarioAtualizado.Email;
            usuario.Senha = usuarioAtualizado.Senha;
            usuario.DataNascimento = usuarioAtualizado.DataNascimento;

            // Não alteramos a CentralCusto nem o UsuarioId, pois a central de custo não deve ser alterada
            // A CentralCusto permanece associada ao usuário original.

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            return Ok(usuario); // Retorna o usuário atualizado
        }

    }
}
