using System.Runtime.InteropServices;
namespace src.Models;

public class Pessoa
{
    public Pessoa()
    {
        this.Nome = "template";
        this.Idade = 0;
        this.contratos = new List<Contrato>();
        this.ativado = true;
    }

    public Pessoa(string Nome, int Idade, string Cpf)
    {
        this.Nome = Nome;
        this.Idade = Idade;
        this.Cpf = Cpf;
        this.contratos = new List<Contrato>();
        this.ativado = true;
    }

    public int Id { get; set; }
    public string Nome { get; set; }
    public int Idade { get; set; }
    public string? Cpf { get; set; }
    public bool ativado { get; set; }

    public List<Contrato> contratos { get; set; }


}