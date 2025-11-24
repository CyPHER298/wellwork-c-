namespace WellworkGS.DTOs;

public class TarefaResourceDTO
{
    public TarefaReadDTO Data { get; set; }
    public List<LinkDTO> Links { get; set; } = new();
}