namespace Domain.Models
{
    public abstract class Transacao
    {
        public int TransacaoID { get; set; }
        public int TransacaoCriptoID { get; set; }
        public int ContaCorrenteID { get; set; }

        public TransacaoCripto TransacaoCripto { get; set; }
    }
}
