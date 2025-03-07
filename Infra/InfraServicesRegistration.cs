using Application.Interfaces.Serivces;
using Infra.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra
{
    public static class InfraServicesRegistration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configs)
        {

            //Transient: sempre que essa instancia for chamado ela vai uma instancia nova. Sempre que a classe empréstimo for chamada eu crio uma nova instancia
            services.AddTransient<IEmprestimoService, EmprestimoService>();

            return services;
        }
    }
}
