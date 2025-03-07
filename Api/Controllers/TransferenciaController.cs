using Api.Controllers.Common;
using Application.DTOs;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransferenciaController : BaseController
    {
        private readonly ITransferenciaService _service;
        public TransferenciaController(ITransferenciaService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost("transferir-fundos")]
        public async Task<IActionResult> TransferirFundos([FromBody] TransferenciaDto request)
        {
            var result = await _service.TransferirFundosAsync(request);

            return result.Valid ? JsonSuccess() : JsonError(result);
        }
    }
}
