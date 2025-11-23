namespace WellworkGS.DTOs;

public class AlertaCriseReadDTO
{
    public int IdAlertaCrise { get; set; }
    public int IdUsuario { get; set; }
    public int IdGestor { get; set; }
    public DateTime DataHoraAlerta { get; set; }
    public string StatusAlerta { get; set; }
}