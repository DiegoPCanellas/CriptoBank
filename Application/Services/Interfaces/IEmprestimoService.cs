using Application.DTOs;
using Application.DTOs.Common;
using Application.Services.Common.Interfaces;
using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IEmprestimoService : IBaseService<Emprestimo>
    {
        Task<Validation> GerarEmprestimoAsync(EmprestimoDto request);
        Task<decimal> GetValorTotalEmprestimoAsync(int emprestimoID);
    }
}
