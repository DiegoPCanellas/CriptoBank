using Application.DTOs;
using Application.DTOs.Common;

namespace Application.Services.Interfaces
{
    public interface IPessoaService
    {
        Task<Validation> CriarPessoa(RegisterDto request);
        Task<Validation> ValidarLoginAsnyc(LoginDto request);
    }
}
