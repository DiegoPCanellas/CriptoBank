using Data.Common;
using Data.Repositories.Interfaces;
using Domain.Models;

namespace Data.Repositories
{
    public class EmprestimoRepository : BaseRepository<Emprestimo>, IEmprestimoRepository
    {
        public EmprestimoRepository(BankDbContext dbContext) : base(dbContext) 
        {
        }
    }
}
