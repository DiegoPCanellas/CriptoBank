using Data.Common;
using Data.Repositories.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ParcelaRepository : BaseRepository<Parcela>, IParcelaRepository
    {
        public ParcelaRepository(BankDbContext dbContext) : base(dbContext)
        {
        }

        public Task<decimal> GetTotalEmprestimoAsync(int emprestimoID)
        {
            return DbSet.Where(x => x.EmprestimoID == emprestimoID)
                        .SumAsync(x => x.Valor);
                        
        }
    }
}
