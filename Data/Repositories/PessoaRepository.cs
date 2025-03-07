using Data.Common;
using Data.Repositories.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class PessoaRepository : BaseRepository<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(BankDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> ExistePessoaByCPFCNPJAsync(string cpfcnpj)
        {
            return DbSet.AnyAsync(x => x.CPFCNPJ == cpfcnpj);
        }

        public Task<int> GetPessoaIdByCpfCnpjAsync(string cpfcnpj)
        {
            return DbSet.Where(x => x.CPFCNPJ == cpfcnpj)
                        .Select(x => x.PessoaID)
                        .FirstOrDefaultAsync();
        }

        public Task<Pessoa> GetPessoaByCpfCnpjAsync(string cpfcnpj)
        {
            return DbSet.FirstOrDefaultAsync(x => x.CPFCNPJ == cpfcnpj);
        }
    }
}
