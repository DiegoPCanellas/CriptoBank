using Api.Controllers.Common;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaCorrenteController : BaseController
    {
        private readonly IContaCorrenteService _service;

        public ContaCorrenteController(IContaCorrenteService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPut("criar-conta-corrente")]
        public async Task<IActionResult> CriarContaCorrente(string cpfcnpj)
        {
            await _service.CriarContaCorrenteAsync(cpfcnpj);

            return JsonSuccess();
        }

        [Authorize]
        [HttpPost("consultar-contas-correntes")]
        public async Task<IActionResult> ConsultarContasCorrentes(string cpfcnpj)
        {
            var (validation, result) = await _service.GetContaCorrenteByCpfCnpjAsync(cpfcnpj);

            if (validation.Valid)
            {
                var retorno = $"Contas do cpf {cpfcnpj}: {string.Join(", ", result)}";

                return JsonSuccess(retorno);
            }

            return JsonError(validation);
        }
    }
}
