namespace WellworkGS.DTOs;

public class TarefaUpdateDTO
{
    public string TituloTarefa { get; set; }
    public string DescricaoTarefa { get; set; }
    public DateTime? DataHoraTarefa { get; set; }
    public string StatusTarefa { get; set; }
}