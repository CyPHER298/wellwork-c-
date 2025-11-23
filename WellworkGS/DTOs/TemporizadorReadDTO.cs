namespace WellworkGS.DTOs;

public class TemporizadorReadDTO
{
    public int IdTemporizador { get; set; }
    public int IdUsuario { get; set; }
    public string TipoTemporizador { get; set; }
    public int? Duracao { get; set; }
    public DateTime? Inicio { get; set; }
    public DateTime? Fim { get; set; }
    public string StatusTemporizador{ get; set; }
}