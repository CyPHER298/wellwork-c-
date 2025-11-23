namespace WellworkGS.DTOs;

public class TarefaReadDTO
{
    public int IdTarefa { get; set; }
    public string TituloTarefa { get; set; }
    public string DescricaoTarefa { get; set; }
    public DateTime? DataHoraTarefa { get; set; }
    public string StatusTarefa { get; set; }

    public int IdUsuario { get; set; }  
}