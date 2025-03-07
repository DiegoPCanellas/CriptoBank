using Api.Controllers.Common;
using Application.DTOs;
using Application.Services.Interfaces;
using Data.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : BaseController
    {
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IPessoaService _pessoaService;

        public AuthController(IJwtTokenGenerator tokenGenerator, 
                              IPessoaService pessoaService)
        {
            _tokenGenerator = tokenGenerator;
            _pessoaService = pessoaService;
        }

        [HttpPost("register"), AllowAnonymous]
        public async Task<IActionResult> CriarPessoa([FromBody] RegisterDto request)
        {
            var result = await _pessoaService.CriarPessoa(request);

            return result.Valid ? JsonSuccess() : JsonError(result);
        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var result = await _pessoaService.ValidarLoginAsnyc(request);

            string token = _tokenGenerator.GenerateToken(request.CPFCNPJ);

            return result.Valid ? JsonSuccess($"Token gerado: {token}") : JsonError(result);
        }
    }
}
