using Application.DTOs;
using Application.DTOs.Common;
using Application.Services.Common.Interfaces;
using Application.Services.Interfaces;
using AutoMapper;
using Data.Repositories.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class DepositoService : TransacaoBaseService, IDepositoService
    {
        private readonly IDepositoRepository _repository;
        private readonly IContaCorrenteService _contaCorrenteService;
        private readonly IMapper _mapper;

        public DepositoService(IDepositoRepository repository,
                               IContaCorrenteService contaCorrenteService,
                               IMapper mapper,
                               IApiService apiService) : base(repository, 
                                                              apiService)
        {
            _repository = repository;
            _contaCorrenteService = contaCorrenteService;
            _mapper = mapper;
        }

        public async Task<Validation> Depositar(DepositoDto request)
        {
            var validation = await ValidarDeposito(request);

            var transacaoCripto = request.TransacaoCripto;

            if (validation.Valid)
            {
                OpenTransaction();
                try
                {
                    var (valorReal, valorCripto) = await GetValoresCotacaoAtuallAsync(transacaoCripto);

                    await _contaCorrenteService.AtualizarSaldoAsync(request.ContaCorrenteID, valorCripto);

                    var model = _mapper.Map<DepositoDto, Deposito>(request);

                    model.TransacaoCripto.ValorReal = valorReal;
                    model.TransacaoCripto.ValorCripto = valorCripto;

                    model.TransacaoCripto.DataReferencia = DateTime.Now;

                    await _repository.AddAsync(model);
                }
                catch
                {
                    Rollback();
                    throw;
                }
            }

            return validation;
        }

        private async Task<Validation> ValidarDeposito(DepositoDto request)
        {
            var validation = new Validation();
            var transacaoCripto = request.TransacaoCripto;

            if (transacaoCripto.ValorCripto > 0 && transacaoCripto.ValorReal > 0)
            {
                validation.AddError("Não é permitido realizar um depósito com tipos de moedas diferentes.");
            }
            else if (transacaoCripto.ValorCripto == 0 && transacaoCripto.ValorReal == 0)
            {
                validation.AddError("Não é permitido realizar um depósito zerado.");
            }

            if (!await _contaCorrenteService.ExisteContaCorrenteAsync(request.ContaCorrenteID))
            {
                validation.AddError("Conta corrente não encontrada.");
            }

            return validation;
        }
    }
}
