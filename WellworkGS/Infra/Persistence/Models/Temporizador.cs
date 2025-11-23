namespace WellworkGS.Infra.Persistence.Models;

public class Temporizador
{
    public int IdTemporizador { get; set; }
    public int IdUsuario { get; set; }
    public string TipoTemporizador { get; set; } // foco, pausa
    public int? Duracao { get; set; }
    public DateTime? Inicio { get; set; }
    public DateTime? Fim { get; set; }
    public string StatusTemporizador { get; set; }

    // Relacionamento
    public Usuario Usuario { get; set; }
}
