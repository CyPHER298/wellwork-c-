namespace WellworkGS.Infra.Persistence.Models;

public class Gestor
{
    public int IdGestor { get; set; }
    public string NomeGestor { get; set; }
    public string EmailGestor { get; set; }
    public string SenhaGestor { get; set; }
    public string CargoGestor { get; set; }
    public string Departamento { get; set; }

    // Relacionamento
    public List<AlertaCrise> AlertasDeCrise { get; set; }
}
