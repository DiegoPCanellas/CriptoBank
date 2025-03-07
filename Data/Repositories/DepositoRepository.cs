using Data.Common;
using Data.Repositories.Interfaces;
using Domain.Models;

namespace Data.Repositories
{
    public class DepositoRepository : TransacaoBaseRepository, IDepositoRepository
    {
        public DepositoRepository(BankDbContext dbContext) : base(dbContext)
        {
        }
    }
}
