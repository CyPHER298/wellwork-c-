namespace WellworkGS.DTOs;

public class TarefaCreateDTO
{
    public int IdUsuario { get; set; }
    public string TituloTarefa { get; set; }
    public string DescricaoTarefa { get; set; }
    public DateTime? DataHoraTarefa { get; set; }
    public string StatusTarefa { get; set; }
}