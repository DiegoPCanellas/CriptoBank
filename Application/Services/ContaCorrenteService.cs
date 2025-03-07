using Application.DTOs.Common;
using Application.Services.Common;
using Application.Services.Interfaces;
using CriptoBank.Domain.Models;
using Data.Repositories.Interfaces;

namespace Application.Services
{
    public class ContaCorrenteService : BaseService<ContaCorrente>, IContaCorrenteService
    {
        private readonly IContaCorrenteRepository _repository;
        private readonly IPessoaRepository _pessoaRepository;

        public ContaCorrenteService(IContaCorrenteRepository repository, 
                                    IPessoaRepository pessoaRepository) : base(repository)
        {
            _repository = repository;
            _pessoaRepository = pessoaRepository;
        }

        public async Task CriarContaCorrenteAsync(string cpfcnpj)
        {
            var pessoaID = await _pessoaRepository.GetPessoaIdByCpfCnpjAsync(cpfcnpj);
            
            OpenTransaction();
            try
            {
                var model = new ContaCorrente()
                {
                    Saldo = 0,
                    PessoaID = pessoaID
                };

                await _repository.AddAsync(model);
            }
            catch
            {
                Rollback();
                throw;
            }
        }

        public async Task AtualizarSaldoAsync(int contaCorrenteID, decimal valor)
        {            
            var contaCorrente = await _repository.GetByIdAsNoTrackingAsync(contaCorrenteID);
            contaCorrente.Saldo += valor;

            _repository.Update(contaCorrente);
        }

        public Task<bool> ExisteContaCorrenteAsync(int contaCorrenteID)
        {
            return _repository.ExisteContaCorrenteAsync(contaCorrenteID);
        }

        public async Task<(Validation validation, decimal saldo)> EmitirExtratoAsync(int contaCorrenteID)
        {
            decimal saldo = 0;

            var validation = new Validation();

            var contaCorrente = await _repository.GetByIdAsync(contaCorrenteID);

            if (contaCorrente == null)
            {
                validation.AddError("Conta corrente inválida.");
            }
            else
            {
                saldo = contaCorrente.Saldo;
            }

            return (validation, saldo);
        }

        public async Task<(Validation, List<int>)> GetContaCorrenteByCpfCnpjAsync(string cpfcnpj)
        {
            var validation = new Validation();
            var contas = new List<int>();

            var pessoa = await _pessoaRepository.GetPessoaByCpfCnpjAsync(cpfcnpj);

            if (pessoa == null)
            {
                validation.AddError("Pessoa não existente.");
            }
            else
            {
                var contasDB = await _repository.GetContasByPessoaAsync(pessoa.PessoaID);

                contas = contasDB.Select(x => x.ContaCorrenteID).ToList();
            }

            return (validation, contas);
        }
    }
}
