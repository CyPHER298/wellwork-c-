namespace WellworkGS.Infra.Persistence.Models;

public class Usuario
{
    public int IdUsuario { get; set; }
    public string NomeUsuario { get; set; }
    public string EmailUsuario { get; set; }
    public string SenhaUsuario { get; set; }
    public string CargoUsuario { get; set; }
    public string Acessibilidade { get; set; }

    // Relacionamentos
    public List<Tarefa> Tarefas { get; set; }
    public List<Temporizador> Temporizadores { get; set; }
    public List<Meta> Metas { get; set; }
    public List<Lembrete> Lembretes { get; set; }
    public List<AlertaCrise> AlertasDeCrise { get; set; }
}
