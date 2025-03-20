

namespace api.Models;

public class CategoriaSaida
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}
