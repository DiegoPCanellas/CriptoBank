using CriptoBank.Domain.Models;

namespace Domain.Models
{
    public class Transferencia : Transacao
    {
        public int TransferenciaID { get; set; }
        public int ContaCorrenteDestinoID { get; set; }
    }
}
