using Domain.Models;

namespace CriptoBank.Domain.Models
{
    public class ContaCorrente
    {
        public int ContaCorrenteID { get; set; }
        public decimal Saldo { get; set; }
        public int PessoaID { get; set; }

        public List<Deposito> Depositos { get; set; }
        public List<Transferencia> Transferencias { get; set; }
        public List<Emprestimo> Emprestimos { get; set; }

        public List<Cartao> Cartoes { get; set; }
    }
}
