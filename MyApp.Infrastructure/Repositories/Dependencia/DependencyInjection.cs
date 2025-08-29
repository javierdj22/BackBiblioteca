using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Interfaces;
using MyApp.Application.Services;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Repositories;

namespace MyApp.Infrastructure.Intranet.Dependencia
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IPrestamoService, PrestamoService>();
            services.AddScoped<IPrestamoRepository, PrestamoRepository>();

            return services;
        }
    }
}
