using Application.DTOs;
using Application.DTOs.Common;
using Application.Services.Common.Interfaces;
using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IDepositoService : IBaseService<Deposito>
    {
        Task<Validation> Depositar(DepositoDto request);
    }
}
