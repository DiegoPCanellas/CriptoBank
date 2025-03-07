using Application.Services;
using Application.Services.Interfaces;
using Data.Common;
using Data.Repositories;
using Data.Common.Interfaces;
using Data.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Data;
using Application.Profiles;
using Application.Services.Common.Interfaces;
using Application.Services.Common;

namespace Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, ConfigurationManager configs)
        {
            //AutoMapper
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            //JWT
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            //Services
            services.AddScoped<IEmprestimoService, EmprestimoService>();
            services.AddScoped<IPessoaService, PessoaService>();
            services.AddScoped<IContaCorrenteService, ContaCorrenteService>();
            services.AddScoped<IDepositoService, DepositoService>();
            services.AddScoped<ITransferenciaService, TransferenciaService>();
            services.AddSingleton<IApiService, ApiService>();
        
            //Repositories
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IEmprestimoRepository, EmprestimoRepository>();
            services.AddScoped<ITransferenciaRepository, TransferenciaRepository>();
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<IContaCorrenteRepository, ContaCorrenteRepository>();
            services.AddScoped<IDepositoRepository, DepositoRepository>();
            services.AddScoped<IParcelaRepository, ParcelaRepository>();

            //Data initializer
            services.AddInfrastructure(configs);

            return services;
        }
    }
}
