using Api.Controllers.Common;
using Application.DTOs;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmprestimoController : BaseController
    {
        private readonly IEmprestimoService _service;

        public EmprestimoController(IEmprestimoService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost("gerar-emprestimo")]
        public async Task<IActionResult> GerarEmprestimo([FromBody] EmprestimoDto request)
        {
            var result = await _service.GerarEmprestimoAsync(request);

            return result.Valid ? JsonSuccess() : JsonError(result);
        }

        [Authorize]
        [HttpPost("get-valor-total-emprestimo")]
        public async Task<IActionResult> GetValorTotalEmprestimo(int emprestimoID)
        {
            var valor = await _service.GetValorTotalEmprestimoAsync(emprestimoID);

            return JsonSuccess($"Total devido: {valor}ETH");
        }
    }
}
