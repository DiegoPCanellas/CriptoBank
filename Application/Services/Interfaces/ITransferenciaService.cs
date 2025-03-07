using Application.DTOs;
using Application.DTOs.Common;
using Application.Services.Common.Interfaces;
using Data.Common.Interfaces;
using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface ITransferenciaService : ITransacaoBaseService
    {
        Task<Validation> TransferirFundosAsync(TransferenciaDto request);
    }
}
