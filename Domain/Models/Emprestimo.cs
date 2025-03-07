namespace Domain.Models
{
    public class Emprestimo
    {
        public int EmprestimoID { get; set; }
        public decimal ValorContratado { get; set; }
        public DateTime DataLiberacao { get; set; }
        public int QuantidadeParcelas { get; set; }
        public decimal PercentualJurosMensal { get; set; }
        public decimal PercentualJurosAtraso { get; set; }
        public int ContaCorrenteID { get; set; }

        public List<Parcela> Parcelas { get; set; }
    }
}
