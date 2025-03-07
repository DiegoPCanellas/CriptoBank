using Data.Common;
using Data.Repositories.Interfaces;
using Domain.Models;

namespace Data.Repositories
{
    public abstract class TransacaoBaseRepository : BaseRepository<Transacao>, ITransacaoBaseRepository
    {
        public TransacaoBaseRepository(BankDbContext dbContext) : base(dbContext)
        {
        }
    }
}
