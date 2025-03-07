using Application.DTOs;
using Application.DTOs.Common;
using Application.Services.Common;
using Application.Services.Interfaces;
using AutoMapper;
using Data.Repositories.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class EmprestimoService : BaseService<Emprestimo>, IEmprestimoService
    {
        private const decimal ValorEmprestimoMaximoPermitido = 1000;
        private const decimal ValorEmprestimoMinimoPermitido = 1;

        private const decimal JurosEmprestimoMensal = 0.1M;
        private const decimal JurosAtrasoMensal = 0.15M;

        private readonly IEmprestimoRepository _repository;
        private readonly IMapper _mapper;
        private readonly IContaCorrenteService _contaCorrenteService;
        private readonly IParcelaRepository _parcelaRepository;

        public EmprestimoService(IEmprestimoRepository repository,
                                 IMapper mapper,
                                 IContaCorrenteService contaCorrenteService,
                                 IParcelaRepository parcelaRepository) : base(repository)
        {
            _repository = repository;
            _mapper = mapper;
            _contaCorrenteService = contaCorrenteService;
            _parcelaRepository = parcelaRepository;
        }

        public async Task<Validation> GerarEmprestimoAsync(EmprestimoDto request)
        {
            var validation = ValidarEmprestimo(request);

            if (validation.Valid)
            {
                OpenTransaction();
                try
                {
                    var valorPorParcela = request.ValorContratado / request.QuantidadeParcelas;

                    var parcelas = new List<Parcela>();
                    for (int i = 0; i < request.QuantidadeParcelas; i++)
                    {
                        var valorParcelaComJuros = valorPorParcela * (1 + JurosEmprestimoMensal);

                        var parcela = new Parcela
                        {
                            Valor = valorParcelaComJuros,
                            DataVencimento = DateTime.Today.AddMonths(i + 1),
                            StatusID = 0
                        };

                        parcelas.Add(parcela);
                    }

                    var model = _mapper.Map<EmprestimoDto, Emprestimo>(request);

                    model.Parcelas = parcelas;

                    model.PercentualJurosMensal = JurosEmprestimoMensal;
                    model.PercentualJurosAtraso = JurosAtrasoMensal;

                    await _contaCorrenteService.AtualizarSaldoAsync(request.ContaCorrenteID, request.ValorContratado);

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

        public Task<decimal> GetValorTotalEmprestimoAsync(int emprestimoID)
        {
            return _parcelaRepository.GetTotalEmprestimoAsync(emprestimoID);
        }

        private static Validation ValidarEmprestimo(EmprestimoDto request)
        {
            var validation = new Validation();

            if (request.ValorContratado > ValorEmprestimoMaximoPermitido)
            {
                validation.AddError("Valor do empréstimo excede o total permitido.");
            }
            else if (request.ValorContratado < ValorEmprestimoMinimoPermitido)
            {
                validation.AddError("Valor do empréstimo menor do que o mínimo permitido.");
            }

            if (request.QuantidadeParcelas <= 0)
            {
                validation.AddError("Quantidade de parcelas inválidas.");
            }

            return validation;
        }
    }
}
