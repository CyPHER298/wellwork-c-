namespace WellworkGS.Infra.Persistence.Models;

public class Meta
{
    public int IdMeta { get; set; }
    public string TituloMeta { get; set; }
    public string DescricaoMeta { get; set; }
    public int? IdUsuario { get; set; }

    // Relacionamento
    public Usuario Usuario { get; set; }
}
