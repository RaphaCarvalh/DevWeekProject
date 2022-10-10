using System.Collections.Generic;

namespace src.Models;

public class Contrato
{ 
    public Contrato()
    {
        this.DataCriacao = DateTime.Now;
        this.Valor = 0;
        this.tokenId = "000000";
        this.Pago = false;
    }
    public Contrato( string TokenID, double Valor){
        this.DataCriacao = DateTime.Now;
        this.tokenId = TokenID;
        this.Valor = Valor;
        this.Pago = false;
    }
    public int Id { get; set; }
    public DateTime DataCriacao { get; set; }
    public string tokenId { get; set; }
    public double Valor { get; set; }
    public bool Pago { get; set; }
    public int PessoaId { get; set; }
}