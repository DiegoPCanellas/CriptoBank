using CriptoBank.Domain.Models;
using Data.Common;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ContaCorrenteRepository : BaseRepository<ContaCorrente>, IContaCorrenteRepository
    {
        private readonly BankDbContext _dbContext;

        public ContaCorrenteRepository(BankDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> ExisteContaCorrenteAsync(int contaCorrenteID)
        {
            return DbSet.AnyAsync(x => x.ContaCorrenteID == contaCorrenteID);
        }

        public Task<List<ContaCorrente>> GetContasByListIDsAsync(List<int> listIDs)
        {
            return DbSet.Where(x => listIDs.Contains(x.ContaCorrenteID))
                        .ToListAsync();
        }

        public Task<List<ContaCorrente>> GetContasByPessoaAsync(int pessoaID) 
        {
            return DbSet.Where(x => x.PessoaID == pessoaID)
                        .ToListAsync();
        }
    }
}
