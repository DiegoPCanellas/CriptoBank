using Application.DTOs.Common;
using Application.Services.Common.Interfaces;
using CriptoBank.Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IContaCorrenteService : IBaseService<ContaCorrente>
    {
        Task AtualizarSaldoAsync(int contaCorrenteID, decimal valor);
        Task CriarContaCorrenteAsync(string cpfcnpj);
        Task<(Validation validation, decimal saldo)> EmitirExtratoAsync(int contaCorrenteID);
        Task<bool> ExisteContaCorrenteAsync(int contaCorrenteID);
        Task<(Validation, List<int>)> GetContaCorrenteByCpfCnpjAsync(string cpfcnpj);
    }
}
