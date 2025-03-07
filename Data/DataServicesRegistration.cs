using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public static class DataServicesRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configs)
        {
            //Passa a connection string para o dbContext da aplicação
            services.AddDbContext<BankDbContext>(options => options.UseSqlServer(configs.GetConnectionString("ConnectionString")));

            return services;
        }
    }
}
