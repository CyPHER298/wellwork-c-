namespace WellworkGS.DTOs;

public class UsuarioResourceDTO
{
    public UsuarioReadDTO Data { get; set; }
    public List<LinkDTO> Links { get; set; } = new();
}