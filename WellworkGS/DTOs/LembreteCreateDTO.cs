namespace WellworkGS.DTOs;

public class LembreteCreateDTO
{
    public int IdUsuario { get; set; }
    public string TipoLembrete { get; set; }
    public int? Frequencia { get; set; }
    public char Ativo { get; set; }
}