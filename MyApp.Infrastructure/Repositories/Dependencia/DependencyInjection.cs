using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Context;
using MyApp.Infrastructure.Repositories;

namespace MyApp.Infrastructure.Intranet.Dependencia
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // SQL Server
            services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection")));

            // PostgreSQL
            services.AddSingleton<DapperContext>();
            //services.AddDbContext<PostgresDbContext>(options =>
            //    options.UseNpgsql(configuration.GetConnectionString("PostgresConnection")));

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IPrestamoRepository, PrestamoRepository>();

            return services;
        }
    }
}
