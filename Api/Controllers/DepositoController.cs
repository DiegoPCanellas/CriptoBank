using Api.Controllers.Common;
using Application.DTOs;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepositoController : BaseController
    {
        private readonly IDepositoService _service;

        public DepositoController(IDepositoService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost("depositar")]
        public async Task<IActionResult> Depositar([FromBody]DepositoDto request)
        {
            var result = await _service.Depositar(request);

            return result.Valid ? JsonSuccess() : JsonError(result);
        }
    }
}
