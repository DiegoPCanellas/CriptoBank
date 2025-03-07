using Api.Controllers.Common;
using Application.DTOs;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExtratoController : BaseController
    {
        private readonly IContaCorrenteService _service;

        public ExtratoController(IContaCorrenteService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost("emitir-extrato")]
        public async Task<IActionResult> EmitirExtrato([FromBody] ExtratoDto request)
        {
            var (result, saldo) = await  _service.EmitirExtratoAsync(request.ContaCorrenteID);

            return result.Valid ? JsonSuccess($"Saldo em conta: {saldo}ETH") : JsonError(result);
        }
    }
}