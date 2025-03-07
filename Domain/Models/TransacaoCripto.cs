namespace Domain.Models
{
    public class TransacaoCripto
    {
        public int TransacaoCriptoID { get; set; }
        public DateTime DataReferencia { get; set; }
        public decimal ValorReal { get; set; }
        public decimal ValorCripto { get; set; }
    }
}
