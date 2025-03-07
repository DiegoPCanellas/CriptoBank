namespace Domain.Models
{
    public class Parcela
    {
        public int ParcelaID { get; set; }
        public decimal Valor { get; set; }
        public byte StatusID { get; set; }
        public int EmprestimoID { get; set; }
        public DateTime DataVencimento { get; set; }
    }
}
