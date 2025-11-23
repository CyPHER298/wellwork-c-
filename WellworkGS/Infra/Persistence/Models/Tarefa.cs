namespace WellworkGS.Infra.Persistence.Models;

public class Tarefa
{
    public int IdTarefa { get; set; }
    public int IdUsuario { get; set; }
    public string TituloTarefa { get; set; }
    public string DescricaoTarefa { get; set; }
    public DateTime? DataHoraTarefa { get; set; }
    public string StatusTarefa { get; set; } // pendente, concluida

    // Relacionamento
    public Usuario Usuario { get; set; }
}
