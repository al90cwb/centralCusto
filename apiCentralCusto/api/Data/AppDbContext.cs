using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.model;

public class AppDbContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<CentralCusto> CentralCustos { get; set; }
    public DbSet<CategoriaEntrada> CategoriaEntradas { get; set; }
    public DbSet<CategoriaSaida> CategoriaSaidas { get; set; }
    public DbSet<LancamentoEntrada> LancamentoEntradas { get; set; }
    public DbSet<LancamentoSaida> LancamentoSaidas { get; set; }

     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=centralCustoDb.db");
    }

    // Configuração da relação entre Usuario e CentralCusto
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração de deleção em cascata
        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.CentralCusto) // Definindo a relação
            .WithOne(c => c.Usuario) // Relacionando com a central de custo
            .HasForeignKey<CentralCusto>(c => c.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade); // Define que, ao excluir um usuário, a central de custo será excluída também
    }
}
