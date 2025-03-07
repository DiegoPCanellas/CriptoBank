using Application.DTOs;
using Application.DTOs.Common;
using Application.Services.Common;
using Application.Services.Interfaces;
using Data.Repositories.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class PessoaService : BaseService<Pessoa>, IPessoaService
    {
        private readonly IPessoaRepository _repository;

        public PessoaService(IPessoaRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Validation> CriarPessoa(RegisterDto request)
        {
            var validation = await ValidarPessoaAsync(request);

            if (validation.Valid)
            {
                OpenTransaction();
                try
                {
                    var model = new Pessoa()
                    {
                        CPFCNPJ = request.CPFCNPJ,
                        Nome = request.Nome,
                        Endereco = request.Endereco,
                        Senha = request.Senha
                    };

                    await _repository.AddAsync(model);
                }
                catch 
                { 
                    Rollback(); 
                    throw; 
                }
            }

            return validation;
        }
        
        public async Task<Validation> ValidarLoginAsnyc(LoginDto request)
        {
            var validation = new Validation();

            var pessoa = await _repository.GetPessoaByCpfCnpjAsync(request.CPFCNPJ);

            if (pessoa == null)
            {
                validation.AddError("Este CPF/CNPJ não existe no sistema.");
            }
            else
            {
                if (pessoa.Senha != request.Senha)
                {
                    validation.AddError("Senha inválida");
                }
            }

            return validation;
        }


        private async Task<Validation> ValidarPessoaAsync(RegisterDto request)
        {
            var validation = new Validation();

            if (string.IsNullOrEmpty(request.CPFCNPJ))
            {
                validation.AddError("O CPF/CNPJ é obrigatório para o cadastro.");
            }
            
            if (string.IsNullOrEmpty(request.Nome))
            {
                validation.AddError("O Nome é obrigatório para o cadastro.");
            }
            
            if (string.IsNullOrEmpty(request.Senha))
            {
                validation.AddError("A senha é obrigatório para o cadastro.");
            }

            if (validation.Valid)
            {
                if (await _repository.ExistePessoaByCPFCNPJAsync(request.CPFCNPJ))
                {
                    validation.AddError("Já existe uma pessoa com este CPFCNPJ.");
                }
            }

            return validation;
        }
    }
}
