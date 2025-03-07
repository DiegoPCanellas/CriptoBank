using Application.DTOs;
using Application.DTOs.Common;
using Application.Services.Common.Interfaces;
using Application.Services.Interfaces;
using AutoMapper;
using CriptoBank.Domain.Models;
using Data.Repositories.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class TransferenciaService : TransacaoBaseService, ITransferenciaService
    {
        private readonly ITransferenciaRepository _repository;
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        private readonly IMapper _mapper;

        public TransferenciaService(ITransferenciaRepository repository,
                                    IContaCorrenteRepository contaCorrenteRepository,
                                    IMapper mapper,
                                    IApiService apiService) : base(repository, 
                                                                   apiService)
        {
            _repository = repository;
            _contaCorrenteRepository = contaCorrenteRepository;
            _mapper = mapper;
        }

        public async Task<Validation> TransferirFundosAsync(TransferenciaDto request)
        {
            var contasIDs = new List<int>()
            {
                request.ContaCorrenteID, 
                request.ContaCorrenteDestinoID
            };

            var contas = await _contaCorrenteRepository.GetContasByListIDsAsync(contasIDs);

            var contaDestino = contas.First(x => x.ContaCorrenteID == request.ContaCorrenteDestinoID);

            var contaOrigem = contas.First(x => x.ContaCorrenteID == request.ContaCorrenteID);

            var validation = ValidarTransferencia(contaOrigem, contaDestino, request);

            if (validation.Valid)
            {
                OpenTransaction();
                try
                {
                    var model = _mapper.Map<TransferenciaDto, Transferencia>(request);

                    var transacaoCripto = request.TransacaoCripto;

                    var (valorReal, valorCripto) = await GetValoresCotacaoAtuallAsync(transacaoCripto);

                    contaDestino.Saldo += valorCripto;
                    contaOrigem.Saldo -= valorCripto;

                    _contaCorrenteRepository.Update(contaDestino);
                    _contaCorrenteRepository.Update(contaOrigem);

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

        private static Validation ValidarTransferencia(ContaCorrente contaOrigem, ContaCorrente contaDestino, TransferenciaDto transferencia)
        {
            var validation = new Validation();

            var transacaoCripto = transferencia.TransacaoCripto;

            if (contaOrigem.Saldo < transacaoCripto.ValorCripto)
            {
                validation.AddError("Saldo insuficiente para realizar a transferência.");
            }

            if (contaOrigem.ContaCorrenteID == contaDestino.ContaCorrenteID)
            {
                validation.AddError("Não é permitido fazer transferências para contas iguais.");
            }

            return validation;
        }
    }
}
