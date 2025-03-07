using CriptoBank.Domain.Models;
using Data.Common.Interfaces;

namespace Data.Repositories.Interfaces
{
    public interface IContaCorrenteRepository : IBaseRepository<ContaCorrente>
    {
        Task<bool> ExisteContaCorrenteAsync(int contaCorrenteID);
        Task<List<ContaCorrente>> GetContasByListIDsAsync(List<int> listIDs);
        Task<List<ContaCorrente>> GetContasByPessoaAsync(int pessoaID);
    }
}
