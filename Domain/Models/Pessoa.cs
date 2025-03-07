using CriptoBank.Domain.Models;

namespace Domain.Models
{
    public class Pessoa
    {
        public int PessoaID { get; set; }
        public string Senha { get; set; }
        public string Endereco { get; set; }
        public string CPFCNPJ { get; set; }
        public string Nome { get; set; }

        public List<ContaCorrente> ContaCorrente { get; set; }
    }
}
