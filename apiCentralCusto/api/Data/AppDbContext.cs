using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<CentralCusto> CentralCustos { get; set; }
    public DbSet<CategoriaEntrada> CategoriaEntradas { get; set; }
    public DbSet<CategoriaSaida> CategoriaSaidas { get; set; }
    public DbSet<LancamentoEntrada> LancamentoEntradas { get; set; }
    public DbSet<LancamentoSaida> LancamentoSaidas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.CentralCusto)
            .WithOne(c => c.Usuario)
            .HasForeignKey<CentralCusto>(c => c.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
