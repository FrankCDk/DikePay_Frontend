using DikePay.Repositories.Implementations;
using DikePay.Repositories.Interfaces;
using DikePay.Services.Implementations;
using DikePay.Services.Interfaces;
using DikePay.State;
using Polly;

namespace DikePay.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {

            services.AddScoped<IDataBaseContext, DataBaseContext>();

            services.AddScoped<IArticuloRepository, ArticuloRepository>();

            // Registramos todos los servicios de API aquí
            services.AddScoped<IArticuloApiService, ArticuloApiService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ISyncService, SyncService>();

            #region Configuramos el HttpClient con Polly
            services.AddHttpClient("DikePayApi", client =>
            {
                client.BaseAddress = new Uri("https://localhost:44397/"); // Cambia esto por tu URL real
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddTransientHttpErrorPolicy(policy =>
                policy.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(2)));
            #endregion

            // Registramos el estado de la aplicación como singleton
            services.AddSingleton<AppState>();

            return services;
        }
    }
}
