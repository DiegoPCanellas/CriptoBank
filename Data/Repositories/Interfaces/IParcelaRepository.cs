using Data.Common.Interfaces;
using Domain.Models;

namespace Data.Repositories.Interfaces
{
    public interface IParcelaRepository : IBaseRepository<Parcela>
    {
        Task<decimal> GetTotalEmprestimoAsync(int emprestimoID);
    }
}
