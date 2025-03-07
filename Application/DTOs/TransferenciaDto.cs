namespace Application.DTOs
{
    public class TransferenciaDto : TransacaoDto
    {
        public int TransferenciaID { get; set; }
        public int ContaCorrenteDestinoID { get; set; }
    }
}
