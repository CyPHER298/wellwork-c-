namespace WellworkGS.Infra.Persistence.Models;

public class Lembrete
{
    public int IdLembrete { get; set; }
    public int IdUsuario { get; set; }
    public string TipoLembrete { get; set; }
    public int? Frequencia { get; set; }
    public char Ativo { get; set; } // 'S' ou 'N'

    // Relacionamento
    public Usuario Usuario { get; set; }
}
