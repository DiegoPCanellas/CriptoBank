using Data.Common;
using Data.Repositories.Interfaces;
using Domain.Models;

namespace Data.Repositories
{
    public class TransferenciaRepository : TransacaoBaseRepository, ITransferenciaRepository
    {
        public TransferenciaRepository(BankDbContext context) : base(context)
        {
        }
    }
}
