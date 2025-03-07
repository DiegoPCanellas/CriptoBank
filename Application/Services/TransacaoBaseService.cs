using Application.DTOs;
using Application.Services.Common;
using Application.Services.Common.Interfaces;
using Application.Services.Interfaces;
using Data.Repositories.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public abstract class TransacaoBaseService : BaseService<Transacao>, ITransacaoBaseService
    {
        private readonly ITransacaoBaseRepository _repository;
        private readonly IApiService _apiService;
        public TransacaoBaseService(ITransacaoBaseRepository repository, 
                                IApiService apiService) : base(repository)
        {
            _repository = repository;
            _apiService = apiService;
        }

        //Este método considera que algum dos valores deve estar preenchido (regra de negócio) e os dois não podem estar preenchidos ao mesmo tempo.
        protected async Task<(decimal ValorReal, decimal ValorCripto)> GetValoresCotacaoAtuallAsync(TransacaoCriptoDto transacaoCripto)
        {
            decimal valorCripto = transacaoCripto.ValorCripto, 
                    valorReal = transacaoCripto.ValorReal;

            var precoEthEmReal = await _apiService.GetCurrentEthPrice();

            if (transacaoCripto.ValorReal > 0)
            {
                valorCripto = transacaoCripto.ValorReal / precoEthEmReal;
            }
            else
            {
                valorReal = transacaoCripto.ValorCripto * precoEthEmReal;
            }

            return (valorReal, valorCripto);
        }

    }
}
