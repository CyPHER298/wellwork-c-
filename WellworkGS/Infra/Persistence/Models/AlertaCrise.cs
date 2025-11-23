namespace WellworkGS.Infra.Persistence.Models;

public class AlertaCrise
{
    public int IdAlertaCrise { get; set; }
    public int IdUsuario { get; set; }
    public int IdGestor { get; set; }
    public DateTime DataHoraAlerta { get; set; }
    public string StatusAlerta { get; set; } // ativo, resolvido

    // Relacionamentos
    public Usuario Usuario { get; set; }
    public Gestor Gestor { get; set; }
}
