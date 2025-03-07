using Application.DTOs;
using AutoMapper;
using CriptoBank.Domain.Models;
using Domain.Models;

namespace Application.Profiles
{
    //AutoMapper para converter models em Dtos
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EmprestimoDto, Emprestimo>().ReverseMap();
            CreateMap<TransacaoDto, Transacao>().ReverseMap();
            CreateMap<TransferenciaDto, Transferencia>().ReverseMap();
            CreateMap<TransacaoCriptoDto, TransacaoCripto>().ReverseMap();
            CreateMap<ContaCorrenteDto, ContaCorrente>().ReverseMap();
            CreateMap<DepositoDto, Deposito>().ReverseMap();
        }
    }
}
