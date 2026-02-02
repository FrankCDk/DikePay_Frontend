using DikePay.Application.Interfaces.Repositories;
using DikePay.Application.Interfaces.Services;
using DikePay.Application.Services;
using DikePay.Shared.State;
using Microsoft.Extensions.DependencyInjection;

namespace DikePay.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {            
            services.AddScoped<ISyncWorkerService, SyncWorkerService>();
            services.AddScoped<IArticuloService, ArticuloService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ISyncService, SyncService>();
            services.AddScoped<IVersionService, VersionService>();
            services.AddScoped<IAlmacenamientoService, AlmacenamientoService>();
            services.AddScoped<IComandaRepository, ComandaService>();
            
            // Registramos el estado de la aplicación como singleton
            services.AddSingleton<AppState>();

            return services;
        }
    }
}
