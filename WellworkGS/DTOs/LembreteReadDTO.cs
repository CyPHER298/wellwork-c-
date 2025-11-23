namespace WellworkGS.DTOs;

public class LembreteReadDTO
{
    public int IdLembrete { get; set; }
    public int IdUsuario { get; set; }
    public string TipoLembrete { get; set; }
    public int? Frequencia { get; set; }
    public char Ativo { get; set; }
}