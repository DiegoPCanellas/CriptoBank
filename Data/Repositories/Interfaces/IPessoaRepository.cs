using Data.Common.Interfaces;
using Domain.Models;

namespace Data.Repositories.Interfaces
{
    public interface IPessoaRepository : IBaseRepository<Pessoa>
    {
        Task<bool> ExistePessoaByCPFCNPJAsync(string cpfcnpj);
        Task<Pessoa> GetPessoaByCpfCnpjAsync(string cpfcnpj);
        Task<int> GetPessoaIdByCpfCnpjAsync(string cpfcnpj);
    }
}
